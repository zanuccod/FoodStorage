using System;
using System.Collections.Generic;
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

        public void AddPack(Pack pack)
        {
            model.InsertPack(pack);
        }

        public void DeletePack(long packId)
        {
            model.DeletePack(packId);
        }

        public void UpdatePack(long packId, Pack pack)
        {
            pack.Id = packId;
            model.UpdatePack(pack);
        }

        public Pack GetPack(long packId)
        {
            return model.GetPack(packId);
        }

        public List<Pack> GetPackList()
        {
            return model.GetPackList();
        }

        public bool IsPackComplete(Pack pack)
        {
            return pack.IsComplete();
        }

        public Pack RemoveItemFromPack(Pack pack)
        {
            if (pack.RemainigItems > 1)
            {
                pack.RemainigItems--;
                model.UpdatePack(pack);
                return pack;
            }
            model.DeletePack(pack.Id);
            return null;
        }
    }
}
