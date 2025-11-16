using Be3.Health.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Be3.Health.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Convenio> Convenios => Set<Convenio>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------- PACIENTE ----------
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Pacientes");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Sobrenome)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.CPF)
                    .HasMaxLength(11);

                entity.HasIndex(p => p.CPF)
                    .IsUnique()
                    .HasFilter("[CPF] IS NOT NULL");

                entity.Property(p => p.Email)
                    .HasMaxLength(200);

                entity.Property(p => p.UFRg)
                    .HasMaxLength(2);

                // Exclusão lógica
                entity.HasQueryFilter(p => p.IsActive);
            });

            // ---------- CONVENIO ----------
            modelBuilder.Entity<Convenio>(entity =>
            {
                entity.ToTable("Convenios");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nome)
                    .HasMaxLength(120)
                    .IsRequired();
            });
        }
    }
}
