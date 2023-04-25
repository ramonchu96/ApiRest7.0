using Api_Ayanet_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Ayanet_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Aggregate models here 
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Direcciones> Direcciones { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
