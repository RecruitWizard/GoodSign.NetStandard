using Newtonsoft.Json;
using System;

namespace GoodSign.NetStandard.Models
{
    public class Document
    {
        [JsonProperty("uuid")]
        public Guid ID { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
