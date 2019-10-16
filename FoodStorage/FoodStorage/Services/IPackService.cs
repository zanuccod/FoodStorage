using System;
using System.Collections.Generic;
using FoodStorage.Entities;

namespace FoodStorage.Services
{
    public interface IPackService
    {
        List<Pack> GetPackList();
        void AddPack(Pack pack);
        Pack GetPack(long packId);
        void RemoveItemFromPack(long packId);
        bool IsPackComplete(long packId);
    }
}
