using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace SCKK_App.Requests
{
    public class BaseRequest
    {
        protected string GetPost(string Url, params string[] postdata)
        {
            var result = string.Empty;
            var data = string.Empty;
            var host = "http://87.229.71.17/";
            //var host = "http://127.0.0.1/";
            host += Url;

            if (postdata.Length % 2 != 0)
            {
                MessageBox.Show("Hiba a paraméterlistában!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }

            for (int i = 0; i < postdata.Length; i += 2)
            {
                data += string.Format("&{0}={1}", postdata[i], postdata[i + 1]);
            }
            data.Remove(0, 1);

            byte[] bytesarr = Encoding.UTF8.GetBytes(data);
            try
            {
                WebRequest request = WebRequest.Create(host);

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
            Errors.Add("MYSQL_ERROR_307", "Adatbázis hiba! Keresd fel a rendszergazdát! Hibakód: 307");
            Errors.Add("MYSQL_ERROR_308", "Adatbázis hiba! Keresd fel a rendszergazdát! Hibakód: 308");
            Errors.Add("MYSQL_ERROR_309", "Adatbázis hiba! Keresd fel a rendszergazdát! Hibakód: 309");
            Errors.Add("UNAUTHENTICATED", "Nem vagy bejelentkezve!");
            Errors.Add("REGEX_NOT_MATCH", "A szöveg nem megfelelő karaktereket tartalmaz!");
            Errors.Add("NO_PERMISSION", "Nincs jogosúltságod ehez a művelethez!");
            Errors.Add("NOT_OWN", "Ez nem a te tulajdonod! :P");
            Errors.Add("UPLOAD_LIMIT_REACHED", "Elérted a feltöltési limited!");

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
