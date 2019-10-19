using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodStorage.Entities;

namespace FoodStorage.Services
{
    public interface IPackService
    {
        Task<List<Pack>> GetPackList();
        Task AddPack(Pack pack);
        Task DeletePack(long packId);
        Task UpdatePack(long packId, Pack pack);
        Task<Pack> GetPack(long packId);
        Task<Pack> RemoveItemFromPack(Pack pack);
        Task<bool> IsPackComplete(Pack pack);
    }
}
