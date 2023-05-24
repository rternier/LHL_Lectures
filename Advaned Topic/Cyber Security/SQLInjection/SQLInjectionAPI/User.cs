namespace SQLInjectionAPI
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Salt { get; set; }

    }

    public class Auth    
    {
        
        public string EmailAddress{ get; set; }
        public string Password { get; set; }        

    }

    public class UserSearch
    {        public string Name { get; set; }      

    }
}
