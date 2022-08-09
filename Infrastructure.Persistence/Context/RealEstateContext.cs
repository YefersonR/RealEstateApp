using Core.Domain.Commons;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        { }
        public DbSet<Estates> Estates { get; set; }
        public DbSet<EstatesImg> EstatesImg { get; set; }
        public DbSet<EstateTypes> EstateTypes { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<SellTypes> SellTypes { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estates>().ToTable("Estates");
            modelBuilder.Entity<EstatesImg>().ToTable("EstatesImg");
            modelBuilder.Entity<EstateTypes>().ToTable("EstateTypes");
            modelBuilder.Entity<Favorites>().ToTable("Favorites");
            modelBuilder.Entity<Features>().ToTable("Features");
            modelBuilder.Entity<SellTypes>().ToTable("SellTypes");

            modelBuilder.Entity<Estates>().HasKey(x => x.Id);
            modelBuilder.Entity<EstatesImg>().HasKey(x => x.Id);
            modelBuilder.Entity<EstateTypes>().HasKey(x => x.Id);
            modelBuilder.Entity<Favorites>().HasKey(x => x.Id);
            modelBuilder.Entity<SellTypes>().HasKey(x => x.Id);
            modelBuilder.Entity<Features>().HasKey(x => x.Id);

            modelBuilder.Entity<Estates>().HasMany<EstatesImg>(x => x.EstatesImgs).WithOne(x => x.Estates).HasForeignKey(x => x.EstatesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EstateTypes>().HasMany<Estates>(x => x.Estates).WithOne(x => x.EstateTypes).HasForeignKey(x => x.EstateTypesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Estates>().HasMany<Favorites>(x => x.Favorites).WithOne(x => x.Estates).HasForeignKey(x => x.EstateId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Features>().HasMany<Estates>(x => x.Estates).WithOne(x => x.Features).HasForeignKey(x => x.FeaturesId)
              //  .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SellTypes>().HasMany<Estates>(x => x.Estates).WithOne(x => x.SellTypes).HasForeignKey(x => x.SellTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
