using Microsoft.EntityFrameworkCore;
using PPS_DLL.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Data
{
    public class ProjetContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=207.154.226.196;User Id=sa;Password=UPYbMz62;Database=projet_prog_sys_PPCD;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data.DAO.Compose>().HasKey(i => new { i.Id_Scenario, i.Id_Actions });
        }

        public DbSet<Scenario> Scenario { get; set; }
        public DbSet<Compose> Compose { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<Recipe> Recipe { get; set; }

    }
}
