using Newtonsoft.Json;
using SCKK_App.Models;
using System.Collections.Generic;

namespace SCKK_App.Requests
{
    public class HomeRequest : BaseRequest
    {
        public List<HomeModel> GetUserData(string sessionCode)
        {
            string result = GetPost("controllers/home.php", "action", "getUserData", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<HomeModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public string GetMessage()
        {
            string result = GetPost("controllers/home.php", "action", "getMessage");
            if (result.StartsWith("OK"))
            {
                return result.Split('|')[1];
            }
            return null;
        }
    }
}
