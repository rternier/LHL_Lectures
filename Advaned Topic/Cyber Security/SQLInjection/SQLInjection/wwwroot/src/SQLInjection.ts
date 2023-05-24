
const API_BASE_URL = "https://localhost:5001";

class SQLInjection {

    constructor() {
        //let btnSI1 = document.getElementById("postSI1");
        //btnSI1.addEventListener("click", (e: Event) => this.loginUser());

        let formSI1 = document.getElementById("formSI1") as HTMLFormElement;
        formSI1.addEventListener("submit", (e: Event) => {
            e.preventDefault();
            this.loginUser();
        });
    }

  

    async loginUser() {
   
        try {
   
            const auth = {
                EmailAddress: (document.getElementById("usernameT1") as HTMLInputElement).value,
                Password: (document.getElementById("passwordT1") as HTMLInputElement).value,
            };

   

            const url = `${API_BASE_URL}/User/Authenticate`;

            //Much Secure
            const headers = new Headers({
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, DELETE',
                'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
            });

 
            const response = await fetch(url, {
                method: 'POST',
                headers: headers,
                body: JSON.stringify(auth)
            });

            const data = await response.json();

            console.log(data);

        }
        catch (error) {
            if (error.response && error.response.status === 401) {
                throw new Error("Invalid username or password");
            } else {
                throw new Error(error.message || "Error occurred during login");
            }
        }
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const sqlInjectionInstance = new SQLInjection();
});