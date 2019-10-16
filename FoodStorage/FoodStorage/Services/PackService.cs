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

        public Pack GetPack(long packId)
        {
            return model.GetPack(packId);
        }

        public List<Pack> GetPackList()
        {
            return model.GetAllPacks();
        }

        public bool IsPackComplete(long packId)
        {
            var pack = model.GetPack(packId);
            return pack.TotalItems == pack.RemainigItems;
        }

        public void RemoveItemFromPack(long packId)
        {
            var pack = model.GetPack(packId);
            if (pack.RemainigItems > 1)
            {
                pack.RemainigItems--;
                model.UpdatePack(pack);
            }
            else
            {
                model.DeletePack(pack.Id);
            }
        }
    }
}
