using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace SCKK_App.Requests
{
    public class BaseRequest
    {
        protected string GetPost(string Url, params string[] postdata)
        {
            var result = string.Empty;
            var data = string.Empty;
            var ascii = new ASCIIEncoding();

            if (postdata.Length % 2 != 0)
            {
                MessageBox.Show("Parameters must be even , \"user\" , \"value\" , ... etc", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }

            for (int i = 0; i < postdata.Length; i += 2)
            {
                data += string.Format("&{0}={1}", postdata[i], postdata[i + 1]);
            }
            data.Remove(0, 1);

            byte[] bytesarr = Encoding.ASCII.GetBytes(data);
            try
            {
                WebRequest request = WebRequest.Create(Url);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytesarr.Length;

                Stream streamwriter = request.GetRequestStream();
                streamwriter.Write(bytesarr, 0, bytesarr.Length);
                streamwriter.Close();

                WebResponse response = request.GetResponse();
                streamwriter = response.GetResponseStream();

                StreamReader streamread = new StreamReader(streamwriter);
                result = streamread.ReadToEnd();
                streamread.Close();

                if (!result.StartsWith("OK"))
                    CheckError(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        protected void RaiseError(string error)
        {
            MessageBox.Show(error, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected int CheckError(string error)
        {
            var Errors = new Dictionary<string, string>();

            Errors.Add("INVALID_KEY", "Rossz kulcsot adtál meg, vagy már lejárt!");
            Errors.Add("MISSING_PARAMETERS", "Tölts ki minden mezőt!");
            Errors.Add("USERNAME_TAKEN", "Ez a felhasználónév foglalt!");
            Errors.Add("BAD_LOGIN", "Rossz bejelentkezési adatok!");
            Errors.Add("PASSWORDS_NOT_MATCH", "A két jelszó nem egyezik!");
            Errors.Add("LOTS_OF_BAD_LOGINS", "Túl sokszor próbáltad! Próbáld újra később!");

            if (!error.StartsWith("ERROR"))
            {
                RaiseError(error);
                return 0;
            }

            string message = "Undefined error";
            string[] array = error.Split(':');
            if (array.Length == 2 && Errors.ContainsKey(array[1]))
            {
                string key = array[1];
                message = Errors[key];
            }

            RaiseError(message);
            return 1;
        }

        protected string GetHash(string data)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));
                var builder = new StringBuilder();

                for (int i = 0; i < hashdata.Length; i++)
                {
                    builder.Append(hashdata[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
