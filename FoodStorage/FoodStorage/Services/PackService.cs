using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodStorage.Entities;
using FoodStorage.Models;

namespace FoodStorage.Services
{
    public class PackService : IPackService
    {
        private readonly IPackDataModel model;

        public PackService(IPackDataModel model)
        {
            this.model = model;
        }

        public async Task AddPack(Pack pack)
        {
            await model.InsertPack(pack);
        }

        public async Task DeletePack(long packId)
        {
            await model.DeletePack(packId);
        }

        public async Task UpdatePack(long packId, Pack pack)
        {
            pack.Id = packId;
            await model.UpdatePack(pack);
        }

        public Task<Pack> GetPack(long packId)
        {
            return model.GetPack(packId);
        }

        public Task<List<Pack>> GetPackList()
        {
            return model.GetPackList();
        }

        public async Task<bool> IsPackComplete(Pack pack)
        {
            return await Task.FromResult(pack.IsComplete());
        }

        public async Task<Pack> RemoveItemFromPack(Pack pack)
        {
            if (pack.RemainigItems > 1)
            {
                pack.RemainigItems--;
                await model.UpdatePack(pack);
                return pack;
            }
            await model.DeletePack(pack.Id);
            return null;
        }
    }
}
