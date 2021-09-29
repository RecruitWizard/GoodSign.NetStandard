using Newtonsoft.Json;

namespace GoodSign.NetStandard.Models
{
    public class MasterDoc
    {
        [JsonProperty("uuid")]
        public string DocumentID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        public string cc { get; set; }
        [JsonProperty("Webhook")]
        public string Webhook { get; set; }

        [JsonProperty("download_pdf")]
        public string MainDocumentDownloadUrl { get; set; }

        [JsonProperty("download_zip")]
        public string OtherDocumentsDownloadUrl { get; set; }
    }

    public class GetDocumentResponse
    {
        [JsonProperty("master_doc")]
        public MasterDoc MasterDoc { get; set; }
    }
}