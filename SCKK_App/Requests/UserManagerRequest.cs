using Newtonsoft.Json;
using SCKK_App.Models;
using System.Collections.Generic;

namespace SCKK_App.Requests
{
    public class UserManagerRequest : BaseRequest
    {
        public List<UserModel> GetUsers(string sessionCode)
        {
            string result = GetPost("controllers/usermanager.php", "action", "getUsers", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<UserModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public List<RankModel> GetRanks(string sessionCode)
        {
            string result = GetPost("controllers/usermanager.php", "action", "getRanks", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<RankModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public List<GroupModel> GetGroups(string sessionCode)
        {
            string result = GetPost("controllers/usermanager.php", "action", "getGroups", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<GroupModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public void SetUser(string sessionCode, string userID, string rankID, string groupID)
        {
            string result = GetPost("controllers/usermanager.php", "action", "setUser", "sessionCode", sessionCode, "userID", userID, "rankID", rankID, "groupID", groupID);
        }
    }
}
