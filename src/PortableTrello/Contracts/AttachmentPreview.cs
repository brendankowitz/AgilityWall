using Newtonsoft.Json;

namespace PortableTrello.Contracts
{
    public class AttachmentPreview
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        [JsonProperty("_id")]
        public string Id { get; set; }
        public bool Scaled { get; set; }
    }
}