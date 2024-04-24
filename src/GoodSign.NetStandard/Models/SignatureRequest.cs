using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoodSign.NetStandard.Models
{
    public class SignatureRequest
    {
        public SignatureRequest()
        {
            Signers = new List<Signer>();
            Metadata = new List<KeyValuePair<string, string>>();
        }

        [JsonProperty("uuid")]
        public Guid DocumentID { get; set; }

        [JsonProperty("doc_name")]
        public string DocumentName { get; set; }

        [JsonProperty("webhook")]
        public string Webhook { get; set; }
        
        [JsonProperty("email_external")]
        public bool SuppressGoodSignEmail { get; set; }

        [JsonProperty("signers")]
        public List<Signer> Signers { get; set; }

        [JsonProperty("metadata")]
        public List<KeyValuePair<string,string>> Metadata { get; set; }

        [JsonProperty("email_subject")]
        public string EmailSubject { get; set;}

        [JsonProperty("email_message")]
        public string EmailMessage { get; set; }

        [JsonProperty("email_fromemail")]
        public string EmailFromAddress { get; set; }

        [JsonProperty("email_fromname")]
        public string EmailFromName { get; set; }

        [JsonProperty("send_in_order")]
        public bool SendInOrder { get; set; }
    }

    public class Signer
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }
    }
}