using Newtonsoft.Json;
using System;

namespace GoodSign.NetStandard.Models
{
    public class Template
    {
        [JsonProperty("uuid")]
        public Guid ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("istemplate")]
        public bool IsTemplate { get; set; }
    }
}
