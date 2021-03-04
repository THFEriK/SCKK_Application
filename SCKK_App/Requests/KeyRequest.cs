using Newtonsoft.Json;
using SCKK_App.Models;
using System.Collections.Generic;

namespace SCKK_App.Requests
{
    class KeyRequest : BaseRequest
    {
        public List<KeyModel> GetKeys(string sessionCode)
        {
            string result = GetPost("controllers/registerkeys.php", "action", "getKeys", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<KeyModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public string GenerateKey(string sessionCode)
        {
            string result = GetPost("controllers/registerkeys.php", "action", "generateKey", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return result.Split('|')[1];
            }
            return null;
        }

        public void DeleteKey(string sessionCode, string registerKey)
        {
            GetPost("controllers/registerkeys.php", "action", "deleteKey", "sessionCode", sessionCode, "registerKey", registerKey);
        }
    }
}
