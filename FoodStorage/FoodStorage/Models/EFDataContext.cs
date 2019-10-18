using System;
using System.IO;
using FoodStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStorage.Models
{
    public class EFDataContext : DbContext
    {
        private const string databaseName = "dbEFData.db";

        #region Constructors

        public EFDataContext()
        {
            Init();
        }

        public EFDataContext(DbContextOptions options)
            : base(options)
        {
            Init();
        }

        #endregion

        #region Public Propeties

        public DbSet<Pack> Packs { get; set; }

        #endregion

        #region Public Methods

        public override void Dispose()
        {
            base.Dispose();

            Packs = null;
        }

        #endregion

        #region Private Methods

        private void Init()
        {
            Database.EnsureCreated();
        }

        #endregion

        #region OnConfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                // create data directory to store database file if not exist
                var dataDirPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                Directory.CreateDirectory(dataDirPath);

                // Specify that we will use sqlite and the path of the database here
                optionsBuilder.UseSqlite($"Filename={Path.Combine(dataDirPath, databaseName)}");
            }
        }

        #endregion
    }
}
