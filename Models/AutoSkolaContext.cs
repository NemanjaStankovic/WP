using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class AutoSkolaContext : DbContext
    {   
        public DbSet<Instruktor> Instruktori { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }

        public DbSet<Polaznik> Polaznici { get; set; }
        public DbSet<InstruktorVozilo> InstruktorVozilo { get; set; }
        public AutoSkolaContext(DbContextOptions options):base(options)
        {

        }


    }
}