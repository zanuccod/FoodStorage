using System.Linq;
using System.Collections.Generic;
using FoodStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public async Task<List<Pack>> GetPackList()
        {
            using var db = new EFDataContext(options);
            return await db.Packs.ToListAsync();
        }

        public async Task<Pack> GetPack(long id)
        {
            using var db = new EFDataContext(options);
            return await db.Packs.FindAsync(id);
        }

        public async Task InsertPack(Pack obj)
        {
            using var db = new EFDataContext(options);
            await db.Packs.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdatePack(Pack obj)
        {
            using var db = new EFDataContext(options);
            db.Packs.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeletePack(long id)
        {
            using var db = new EFDataContext(options);
            var item = await db.Packs.FindAsync(id);
            db.Packs.Remove(item);
            await db.SaveChangesAsync();
        }

        #endregion
    }
}
