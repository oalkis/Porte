using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porte.CaseStudy.Models
{
    public class PorteDBContext : DbContext
    {
        public DbSet<Box> Boxs { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Box>().HasData(InitBox());//Case'de verilen değerler veritabanına kaydediliyor.

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlite("Data Source=Porte.db");//Küçük bir proje olduğu için Sqlite kullanıldı.

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        private List<Box> InitBox()
        {

            var boxList = new List<Box>() { };
            boxList.Add(new Box
            {
                BOX_ID = 123450,
                WEIGHT = 3,
                PART_COUNT = 1

            });
            boxList.Add(new Box
            {
                BOX_ID = 123461,
                WEIGHT = 8,
                PART_COUNT = 1

            }); 
            boxList.Add(new Box
            {
                BOX_ID = 123472,
                WEIGHT = 11,
                PART_COUNT = 1

            }); 
            boxList.Add(new Box
            {
                BOX_ID = 123483,
                WEIGHT = 3,
                PART_COUNT = 1

            });
            boxList.Add(new Box
            {
                BOX_ID = 123494,
                WEIGHT = 13,
                PART_COUNT = 1

            });

            return boxList;
        }
    }
}
