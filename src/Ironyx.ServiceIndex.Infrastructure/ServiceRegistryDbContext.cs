using Ironyx.ServiceIndex.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ironyx.ServiceIndex.Infrastructure
{
    public class ServiceRegistryDbContext : DbContext
    {
        public DbSet<RegistrationEntity> Registrations { get; set; }

        public ServiceRegistryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistrationEntity>().CreateRegistrationEntity();

            base.OnModelCreating(modelBuilder);
        }
    }

    file static class ServiceRegistryDbContextExtensions
    {
        public static void CreateRegistrationEntity(this EntityTypeBuilder<RegistrationEntity> builder)
        {
            builder.ToTable("registrations");

            builder.HasKey(b => b.Id);
            builder.HasIndex(b => b.Name)
                .IsUnique();

            builder.Property(b => b.Id)
                .HasColumnName("id");
            builder.Property(b => b.Name)
                .HasColumnName("name")
                .IsRequired();
            builder.Property(b => b.Uri)
                .HasColumnName("uri")
                .IsRequired();

            builder.OwnsMany(e => e.CanonicalTypes, builder => builder.ToJson("canonical_types"));
        }
    }
}
