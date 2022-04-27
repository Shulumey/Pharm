using System.Data.Entity;
using BCC.Pharm.DataAccess.Entities;

namespace BCC.Pharm.DataAccess
{
    public class PharmDbContext : DbContext
    {
        public PharmDbContext(string connectionString)
            :base(connectionString)
        {
        }
        
        public DbSet<Substance> Substances { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<ChangeHistory> ChangesHistory { get; set; }
    }
}