﻿using System.Text.Json.Serialization;

namespace RiotPls.DataDragon.Entities
{
    internal class MissionAssetDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("image")]
        public StaticImageDto Image { get; set; }
    }
}