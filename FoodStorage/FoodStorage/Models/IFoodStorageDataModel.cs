using System;
using System.Collections.Generic;

namespace FoodStorage.Models
{
    public interface IFoodStorageDataModel<T>
    {
        List<T> GetAll();
        void Add(T obj);
        T Get(long id);
        void RemoveItem(long id);
        bool IsComplete(long id);
    }
}
