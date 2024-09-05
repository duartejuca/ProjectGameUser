using System;
using Microsoft.EntityFrameworkCore;
using USERS.API.Models;

namespace USERS.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            var pendingMigrations = Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                Database.Migrate(); // garante que o banco esteja criado ao rodar
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PlayerData> PlayerData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}