using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transactions.Model
{
    public class DBTransContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DBTransContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Transaction>().HasData(new Transaction() { Id = 1, FromUserId = "123", ToUserId = "789", Amount = 22.22 });
            modelBuilder.Entity<Transaction>().HasData(new Transaction() { Id = 2, FromUserId = "123", ToUserId = "789", Amount = 23.22 });
            modelBuilder.Entity<Transaction>().HasData(new Transaction() { Id = 3, FromUserId = "789", ToUserId = "123", Amount = 23.22 });
        }

    }
}
