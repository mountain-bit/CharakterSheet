using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class CharaktersBDContext : DbContext
    {
        public DbSet<Charakter> Charakters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Roll> Rolls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=Komputer-KJ\\MSSQLSERVER01;Database=WPFApp;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
