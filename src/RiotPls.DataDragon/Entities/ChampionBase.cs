﻿using System;
using RiotPls.DataDragon.Enums;

namespace RiotPls.DataDragon.Entities
{
    public class ChampionBase
    {
        /// <summary>
        ///     The version of the data.
        /// </summary>
        public GameVersion Version { get; }

        /// <summary>
        ///     The unique identifier of the champion.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     The unique key of the champion.
        /// </summary>
        public int Key { get; }

        /// <summary>
        ///     The name of the champion.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The title of the champion.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     The summary of the champion.
        /// </summary>
        public string Summary { get; }

        /// <summary>
        ///     The resource type of the champion. (mana, energy, fury)
        /// </summary>
        public ResourceType ResourceType { get; }

        /// <summary>
        ///     General statistics about the champion. 
        ///     Such a power and difficulty.
        /// </summary>
        public ChampionInformation Information { get; }

        /// <summary>
        ///     Information and name of the images associated with that champion.
        /// </summary>
        public StaticImage Image { get; }

        /// <summary>
        ///     Tags representing the champion.
        /// </summary>
        public ChampionType Tags { get; }

        /// <summary>
        ///     Statistics related to the champion.
        /// </summary>
        public ChampionStatistics Statistics { get; }

        internal ChampionBase(ChampionBaseDto dto)
        {
            Version = dto.Version;
            Id = dto.Id;
            Key = int.Parse(dto.Key);
            Name = dto.Name;
            Title = dto.Title;
            Summary = dto.Blurb;
            ResourceType = Enum.Parse<ResourceType>(dto.Partype.Replace(" ", string.Empty), true);
            Information = new ChampionInformation(dto.Info);
            Image = new StaticImage(dto.Image);
            Tags = dto.Tags;
            Statistics = new ChampionStatistics(dto.Stats);
        }
    }
}