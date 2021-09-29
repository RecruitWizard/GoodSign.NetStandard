using Newtonsoft.Json;
using System;

namespace GoodSign.NetStandard.Models
{
    public class Doc
    {
        [JsonProperty("uuid")]
        public Guid ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("webhook")]
        public string Webhook { get; set; }
    }

    public class SignatureResponse
    {
        [JsonProperty("IsSuccessful")]
        public bool success { get; set; }

        [JsonProperty("doc")]
        public Doc Document { get; set; }

        [JsonProperty("warnings")]
        public string Warnings { get; set; }

        [JsonProperty("credit")]
        public int RemainingCredits { get; set; }
    }
}