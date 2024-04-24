using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public decimal RemainingCredits { get; set; }
        
        [JsonProperty("signing_links")]
        public List<SigningLink> SigningLinks { get; set; }
    }

    public class SigningLink
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}