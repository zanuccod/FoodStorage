using System.Linq;
using System.Collections.Generic;
using FoodStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Models
{
    public class EFDataModel : EFDataContext, IPackDataModel
    {
        private readonly DbContextOptions options;

        #region Constructors

        public EFDataModel()
        {
            options = new DbContextOptionsBuilder().Options;
        }

        public EFDataModel(DbContextOptions options)
        {
            this.options = options;
        }

        #endregion

        #region Public Pack Methods

        public List<Pack> GetPackList()
        {
            using var db = new EFDataContext(options);
            return db.Packs.ToList();
        }

        public Pack GetPack(long id)
        {
            using var db = new EFDataContext(options);
            return db.Packs.Find(id);
        }

        public void InsertPack(Pack obj)
        {
            using var db = new EFDataContext(options);
            db.Packs.Add(obj);
            db.SaveChanges();
        }

        public void UpdatePack(Pack obj)
        {
            using var db = new EFDataContext(options);
            db.Packs.Update(obj);
            db.SaveChanges();
        }

        public void DeletePack(long id)
        {
            using var db = new EFDataContext(options);
            var item = db.Packs.Find(id);
            db.Packs.Remove(item);
            db.SaveChanges();
        }

        #endregion
    }
}
