﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FoodStorage.Entities
{
    public class Pack : IEquatable<Pack>
    {
        [JsonProperty("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // autoincrement key
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("remaining_items")]
        public int RemainigItems { get; set; }

        public bool Equals([AllowNull] Pack item)
        {
            if (item == null)
            {
                return false;
            }

            var result = true;
            result &= (Id == item.Id);
            result &= (Name.Equals(item.Name));
            result &= (TotalItems == item.TotalItems);
            result &= (RemainigItems == item.RemainigItems);
            return result;
        }

        public bool IsComplete()
        {
            return TotalItems == RemainigItems;
        }
    }
}
