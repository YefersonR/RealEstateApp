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
        public DbSet<Estate> Estates { get; set; }
        public DbSet<EstatesImg> EstatesImg { get; set; }
        public DbSet<EstateType> EstateTypes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<SellType> SellTypes { get; set; }
        public DbSet<FeaturesRelations> FeaturesRelations { get; set; }

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
            modelBuilder.Entity<Estate>().ToTable("Estates");
            modelBuilder.Entity<EstatesImg>().ToTable("EstatesImg");
            modelBuilder.Entity<EstateType>().ToTable("EstateTypes");
            modelBuilder.Entity<Favorite>().ToTable("Favorites");
            modelBuilder.Entity<Feature>().ToTable("Features");
            modelBuilder.Entity<SellType>().ToTable("SellTypes");
            modelBuilder.Entity<FeaturesRelations>().ToTable("FeaturesRelations");

            modelBuilder.Entity<Estate>().HasKey(x => x.Id);
            modelBuilder.Entity<EstatesImg>().HasKey(x => x.Id);
            modelBuilder.Entity<EstateType>().HasKey(x => x.Id);
            modelBuilder.Entity<Favorite>().HasKey(x => x.Id);
            modelBuilder.Entity<SellType>().HasKey(x => x.Id);
            modelBuilder.Entity<Feature>().HasKey(x => x.Id);
            modelBuilder.Entity<FeaturesRelations>().HasKey(x => x.Id);

            modelBuilder.Entity<Estate>().HasMany<EstatesImg>(x => x.EstatesImgs).WithOne(x => x.Estates).HasForeignKey(x => x.EstatesId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<EstateType>().HasMany<Estate>(x => x.Estates).WithOne(x => x.EstateTypes).HasForeignKey(x => x.EstateTypesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Estate>().HasMany<Favorite>(x => x.Favorites).WithOne(x => x.Estates).HasForeignKey(x => x.EstateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Estate>().HasMany<FeaturesRelations>(x => x.FeaturesRelations).WithOne(x => x.Estates).HasForeignKey(x => x.EstateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SellType>().HasMany<Estate>(x => x.Estates).WithOne(x => x.SellTypes).HasForeignKey(x => x.SellTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
