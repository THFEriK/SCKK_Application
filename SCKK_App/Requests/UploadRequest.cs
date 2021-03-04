using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Text.Json;
using SCKK_App.Models;
using Newtonsoft.Json;
using System.IO;

namespace SCKK_App.Requests
{
    class UploadRequest : BaseRequest
    {
        public void Upload(List<ResultModel> data, string sessionCode, string tablename)
        {
            string stringjson = JsonConvert.SerializeObject(data);

            string result = GetPost("controllers/upload.php", "sessionCode", sessionCode, "data", stringjson, "tablename", tablename);
        }
    }
}
