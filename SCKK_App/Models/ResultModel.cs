using System;

namespace SCKK_App.Models
{
    public class ResultModel
    {
        public DateTime time { get; set; }
        public string name { get; set; }
        public ushort identifier { get; set; }
        public CallModel callId { get; set; }
    }
}
