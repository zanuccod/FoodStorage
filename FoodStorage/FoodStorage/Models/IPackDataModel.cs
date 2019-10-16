using System;
using System.Collections.Generic;
using FoodStorage.Entities;

namespace FoodStorage.Models
{
    public interface IPackDataModel
    {
        List<Pack> GetAllPacks();
        Pack GetPack(long id);
        void InsertPack(Pack obj);
        void UpdatePack(Pack obj);
        void DeletePack(long id);
    }
}
