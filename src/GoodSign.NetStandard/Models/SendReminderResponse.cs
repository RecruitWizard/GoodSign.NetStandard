using Newtonsoft.Json;

namespace GoodSign.NetStandard.Models
{
    public class SendReminderResponse
    {
        [JsonProperty("success")]
        public bool IsSuccessful { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("master_doc")]
        public MasterDoc MasterDoc { get; set; }
    }
}