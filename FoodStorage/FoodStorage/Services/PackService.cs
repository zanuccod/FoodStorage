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
            await model.InsertPack(pack).ConfigureAwait(true);
        }

        public async Task DeletePack(long packId)
        {
            await model.DeletePack(packId).ConfigureAwait(true);
        }

        public async Task UpdatePack(long packId, Pack pack)
        {
            if (pack != null)
            {
                pack.Id = packId;
                await model.UpdatePack(pack).ConfigureAwait(true);
            }
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
            return pack != null ? await Task.FromResult(pack.IsComplete()).ConfigureAwait(true) : false;
        }

        public async Task<Pack> RemoveItemFromPack(Pack pack)
        {
            if (pack != null)
            {
                if (pack.RemainigItems > 1)
                {
                    pack.RemainigItems--;
                    await model.UpdatePack(pack).ConfigureAwait(true);
                    return pack;
                }
                await model.DeletePack(pack.Id).ConfigureAwait(true);
            }
            return null;
        }
    }
}
