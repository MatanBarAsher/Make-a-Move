using Make_a_move___Server.DAL;

namespace Make_a_move___Server.BL
{

    public class LoginCredentials
    {

        private string phone;
        private string password;

        public LoginCredentials(string phone, string password)
        {
            this.phone = phone;
            this.password = password;
        }

        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
    }
}
