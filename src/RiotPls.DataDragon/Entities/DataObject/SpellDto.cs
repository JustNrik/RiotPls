﻿using System.Text.Json.Serialization;
#nullable disable
namespace RiotPls.DataDragon.Entities
{
    internal class SpellDto : SpellBaseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("tooltip")]
        public string ToolTip { get; set; }

        [JsonPropertyName("leveltip")]
        public LevelTipDto LevelTip { get; set; }

        [JsonPropertyName("maxrank")]
        public int MaxRank { get; set; }

        [JsonPropertyName("cooldown")]
        public double[] Cooldowns { get; set; }

        [JsonPropertyName("cooldownBurn")]
        public string CooldownBurn { get; set; }

        [JsonPropertyName("cost")]
        public double[] Costs { get; set; }

        [JsonPropertyName("costBurn")]
        public string CostBurn { get; set; }

        [JsonPropertyName("datavalues")]
        public object DataValues { get; set; }

        [JsonPropertyName("effect")]
        public double[][] Effects { get; set; }

        [JsonPropertyName("effectBurn")]
        public string[] EffectBurn { get; set; }

        [JsonPropertyName("vars")]
        public SpellVarDto[] Vars { get; set; }

        [JsonPropertyName("costType")]
        public string CostType { get; set; }

        [JsonPropertyName("maxammo")]
        public string MaxAmmo { get; set; }

        [JsonPropertyName("range")]
        public int[] Ranges { get; set; }

        [JsonPropertyName("rangeBurn")]
        public string RangeBurn { get; set; }

        [JsonPropertyName("resource")]
        public string Resource { get; set; }
    }
}