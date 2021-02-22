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
        public void Upload(List<ResultModel> data, string sessionCode, string filename)
        {
                string stringjson = JsonConvert.SerializeObject(data);

                if (stringjson.Length > 500000)
                    MessageBox.Show("A feltöltendő fájl mérete túl nagy!");

                if (stringjson.Length < 100)
                    MessageBox.Show("A feltöltendő fájl mérete túl alacsony!");

                string result = GetPost("http://localhost/sckk-php/controllers/upload.php", "sessionCode", sessionCode, "data", stringjson, "filename", filename);
                if (result.StartsWith("OK"))
                {
                    MessageBox.Show("Sikeres feltöltés!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
        }
    }
}
