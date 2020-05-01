﻿namespace RiotPls.DataDragon.Entities
{
    public class ItemBlock
    {
        /// <summary>
        ///     Id of the item.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        ///     Amount of time the item is recommended.
        /// </summary>
        public int Count { get; }

        /// <summary>
        ///     Whether the count is hidden or not.
        /// </summary>
        public bool HideCount { get; }

        internal ItemBlock(ItemBlockDto dto)
        {
            Id = dto.Id;
            Count = dto.Count;
            HideCount = dto.HideCount;
        }
    }
}