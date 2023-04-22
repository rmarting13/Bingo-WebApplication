using Microsoft.EntityFrameworkCore;
using WebApplicationBingo.Models;

namespace WebApplicationBingo.Data.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<HistorialBolilleroViewModel> HistorialBolillero { get; set; }
        public DbSet<HistorialCartonesViewModel> HistorialCartones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=database-name;Initial Catalog=WebAppBingo;Integrated Security=true;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
