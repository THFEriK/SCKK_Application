using System;
using System.Windows;

namespace SCKK_App.Requests
{
    public class AuthenticationRequest : BaseRequest
    {
        public string[] Login(string name, string pwd)
        {
            string result = GetPost("http://localhost/sckk-php/controllers/authenticate.php", "action", "loginUser", "username", name, "password", GetHash(pwd));
            if (result.StartsWith("OK"))
            {
                MessageBox.Show("Sikeres bejelentkezés!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return result.Split(':');
            }
            return null;
        }

        public void Registration(string key, string name, string pwd1, string pwd2)
        {
            if (pwd1.Length > 5)
            {
                string result = GetPost("http://localhost/sckk-php/controllers/authenticate.php", "action", "registerUser", "registerKey", key, "username", name, "password", GetHash(pwd1), "repassword", GetHash(pwd2));
                if (result.StartsWith("OK"))
                {
                    MessageBox.Show("Sikeres regisztráció!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("A jelszó túl rövid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
