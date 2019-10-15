using System.Linq;
using System.Collections.Generic;
using FoodStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Models
{
    public class PackDataModel : EntityFrameworkBase<Pack>, IFoodStorageDataModel<Pack>
    {
        #region Constructors

        public PackDataModel()
        { }

        public PackDataModel(DbContextOptions<EntityFrameworkBase<Pack>> options)
            : base(options)
        { }

        #endregion

        #region Public Methods

        public void Add(Pack obj)
        {
            Table.Add(obj);
            SaveChanges();
        }

        public Pack Get(long id)
        {
            return Table.Find(id);
        }

        public List<Pack> GetAll()
        {
            return Table.OrderByDescending(x => x.Id).ToList();
        }

        public void RemoveItem(long id)
        {
            Table.Where(x => x.Id == id).First().RemainigItems--;
            SaveChanges();
        }

        public bool IsComplete(long id)
        {
            var item = Table.Find(id);
            return item.RemainigItems == item.TotalItems;
        }

        #endregion
    }
}
