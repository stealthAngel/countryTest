
using Microsoft.EntityFrameworkCore;

namespace w4sd.Models
{
    public class SuperContext : DbContext
    {
        public SuperContext(DbContextOptions<SuperContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressCountryRule> AdressCountryRules { get; set; }
    }
}
