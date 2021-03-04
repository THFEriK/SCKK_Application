using System.Windows;

namespace SCKK_App.Requests
{
    public class AuthenticationRequest : BaseRequest
    {
        public string[] Login(string name, string pwd)
        {
            string result = GetPost("controllers/authenticate.php", "action", "loginUser", "username", name, "password", GetHash(pwd));
            if (result.StartsWith("OK"))
            {
                return result.Split(':');
            }
            return null;
        }

        public bool Registration(string key, string name, string pwd1, string pwd2)
        {
            if (pwd1.Length > 5)
            {
                string result = GetPost("controllers/authenticate.php", "action", "registerUser", "registerKey", key, "username", name, "password", GetHash(pwd1), "repassword", GetHash(pwd2));
                if (result.StartsWith("OK"))
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("A jelszó túl rövid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        public string AutoLogin(string sessionCode)
        {
            string result = GetPost("controllers/authenticate.php", "action", "autoLogin", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return result.Split('|')[1];
            }
            return "0";
        }

        public string GetVersion()
        {
            string result = GetPost("controllers/authenticate.php", "action", "getVersion");
            if (result.StartsWith("OK"))
            {
                return result.Split('|')[1];
            }
            return null;
        }
    }
}
