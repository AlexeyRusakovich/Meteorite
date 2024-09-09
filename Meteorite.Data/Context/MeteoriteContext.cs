using Meteorite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Meteorite.Data.Context
{
    public class MeteoriteContext : DbContext
    {
        public MeteoriteContext(DbContextOptions<MeteoriteContext> options) : base(options)
        {
        }
        public DbSet<MeteoriteDb> Meteorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeteoriteDb>()
                .HasKey(e => e.Id);
        }
    }
}
