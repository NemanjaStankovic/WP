using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class AutoSkolaContext : DbContext
    {   
        public DbSet<Instruktor> Instruktori { get; set; }
        public DbSet<Instruktor> Polaznici { get; set; }
        public DbSet<Instruktor> Vozila { get; set; }
        public AutoSkolaContext(DbContextOptions options):base(options)
        {
            
        }


    }
}