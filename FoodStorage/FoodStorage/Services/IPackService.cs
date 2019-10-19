using System;
using System.Collections.Generic;
using FoodStorage.Entities;

namespace FoodStorage.Services
{
    public interface IPackService
    {
        List<Pack> GetPackList();
        void AddPack(Pack pack);
        void DeletePack(long packId);
        void UpdatePack(long packId, Pack pack);
        Pack GetPack(long packId);
        Pack RemoveItemFromPack(Pack pack);
        bool IsPackComplete(Pack pack);
    }
}
