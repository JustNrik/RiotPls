using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class ItemGroupDto
    {
        [JsonPropertyName("header")]
        public string Header { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
    }
}