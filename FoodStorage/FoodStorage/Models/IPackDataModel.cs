using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodStorage.Entities;

namespace FoodStorage.Models
{
    public interface IPackDataModel
    {
        Task<List<Pack>> GetPackList();
        Task<Pack> GetPack(long id);
        Task InsertPack(Pack item);
        Task UpdatePack(Pack item);
        Task DeletePack(long id);
    }
}
