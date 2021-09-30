using Newtonsoft.Json;
using System.Collections.Generic;

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

        [JsonProperty("signers")]
        public List<GetSigner> Signers { get; set; }

        [JsonProperty("download_pdf")]
        public string MainDocumentDownloadUrl { get; set; }

        [JsonProperty("download_zip")]
        public string OtherDocumentsDownloadUrl { get; set; }
    }

    public class GetSigner
    {
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
        [JsonProperty("complete")]
        public bool Complete { get; set; }
        [JsonProperty("reminderdays")]
        public int ReminderDays { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("emailStatus")]
        public string EmailStatus { get; set; }
    }

    public class Contact
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class GetDocumentResponse
    {
        [JsonProperty("master_doc")]
        public MasterDoc MasterDoc { get; set; }
    }
}