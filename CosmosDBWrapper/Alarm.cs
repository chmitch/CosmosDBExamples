using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace CosmosDBWrapper
{
    public class Alarm : Document
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        //[JsonProperty("ETag")]
        //public string ETag { get; }

        [JsonProperty("TenantId")]
        public int TenantId { get; set; }

        [JsonProperty("SomeData")]
        public string SomeData { get; set; }
    }
}
