using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using SCKK_App.Models;

namespace SCKK_App.Requests
{
    class DownloadRequest : BaseRequest
    {
        public List<TableModel> DownloadTableList(string sessionCode)
        {
            string result = GetPost("controllers/download.php", "action", "downloadTableList", "sessionCode", sessionCode);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<TableModel>>(result.Split('|')[1]);
            }
            return null;
        }

        public void DeleteTable(string sessionCode, string tablename)
        {
            string result = GetPost("controllers/download.php", "action", "deleteTable", "sessionCode", sessionCode, "tablename", tablename);
        }

        public List<ResultModel> DownloadLog(string sessionCode, string tablename)
        {
            string result = GetPost("controllers/download.php", "action", "downloadLog", "sessionCode", sessionCode, "tablename", tablename);
            if (result.StartsWith("OK"))
            {
                return JsonConvert.DeserializeObject<List<ResultModel>>(result.Split('|')[1]);
            }
            return null;
        }
    }
}
