using Newtonsoft.Json;
using SCKK_App.Models;
using System.Collections.Generic;

namespace SCKK_App.Requests
{
    class UploadRequest : BaseRequest
    {
        public void Upload(List<ResultModel> data, string sessionCode, string tablename)
        {
            string stringjson = JsonConvert.SerializeObject(data);

            GetPost("controllers/upload.php", "sessionCode", sessionCode, "data", stringjson, "tablename", tablename);
        }
    }
}
