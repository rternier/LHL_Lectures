using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace SQLInjectionAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly string connectionString = "Server=localhost;Database=projects;Trusted_Connection=True;";

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Tries to authenticate a user
        /// </summary>
        /// <param name="authUser">user parameters</param>
        /// <returns>JSON object</returns>
        [HttpPost(Name = "authenticate")]
        public IActionResult Authenticate(Auth authUser)
        {
            if (authUser == null || string.IsNullOrEmpty(authUser.EmailAddress) || string.IsNullOrEmpty(authUser.Password))
            {
                return Json(new { error = "Invalid input" });
            }

            //Regex to check authUser

            //For the sake of this demo we're doing a direct password match. 
            //in the real world you would hash this value, and then compare with the database.

            //Using Concatonated SQL - Easily exploitable by SQLInjection
            //Better - switch to SQL Paramaters
            //Best - Use a stored proc.             
            string sqlCommand = $"SELECT name FROM Projects.dbo.[User] WHERE EmailAddress = {authUser.EmailAddress} AND Password = {authUser.Password}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@emailaddress", authUser.EmailAddress);
                    //command.Parameters.AddWithValue("@password", authUser.Password);
                    connection.Open();

                    var emailAddress = command.ExecuteScalar()?.ToString();

                    if (string.IsNullOrEmpty(emailAddress))
                    {
                        return Json(new { error = "Invalid username or password" });
                    }
                    else
                    {
                        return Json(new { email = emailAddress });
                    }
                }
            }

        }

        [HttpPost(Name = "loginWithParams")]
        public string LoginWithParams([FromBody] Auth user)
        {
            if (user == null || string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Invalid User input");
            }

            string sqlCommand = "SELECT * FROM Projects.dbo.[User] WHERE EmailAddress = @emailaddress AND Password = @password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.AddWithValue("@emailaddress", user.EmailAddress);
                    command.Parameters.AddWithValue("@password", user.Password);

                    connection.Open();
                    var name = command.ExecuteScalar()?.ToString();

                    return name;
                }
            }

        }


        [HttpPost(Name = "loginWithSP")]
        public string loginWithSP([FromBody] Auth user)
        {
            if (user == null || string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Invalid User input");
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("login", connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@emailaddress", user.EmailAddress);
                    command.Parameters.AddWithValue("@password", user.Password);

                    connection.Open();
                    var name = command.ExecuteScalar()?.ToString();

                    return name;
                }
            }

        }


        [HttpPost(Name = "Product")]
        public void Product(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Invalid Product Name");
            }

            string sqlCommand = $"insert into Projects.dbo.Product(name) values (@name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                 

                    connection.Open();
                    command.ExecuteNonQuery();

                    
                }
            }

        }

    }
}
