using Newtonsoft.Json;

namespace PortableTrello.Contracts
{
    public class FieldValue
    {
        [JsonProperty("_value")]
        public string Value { get; set; } 
    }
}