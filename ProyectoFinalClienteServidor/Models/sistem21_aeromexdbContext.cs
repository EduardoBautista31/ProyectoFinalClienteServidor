using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoFinalClienteServidor.Models
{
    public partial class sistem21_aeromexdbContext : DbContext
    {
        public sistem21_aeromexdbContext()
        {
        }

        public sistem21_aeromexdbContext(DbContextOptions<sistem21_aeromexdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Vuelo> Vuelos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado");

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(99)
                    .HasColumnName("Estado");
            });

            modelBuilder.Entity<Vuelo>(entity =>
            {
                entity.HasKey(e => e.IdVuelo)
                    .HasName("PRIMARY");

                entity.ToTable("vuelo");

                entity.HasIndex(e => e.IdEstado, "fkEstado_idx");

                entity.Property(e => e.IdVuelo)
                    .HasColumnType("int(11)")
                    .HasColumnName("idVuelo");

                entity.Property(e => e.CodigoVuelo).HasMaxLength(99);

                entity.Property(e => e.Hora).HasColumnType("time");

                entity.Property(e => e.HoraLlegada).HasColumnType("time");

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.Salida).HasMaxLength(99);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Vuelos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkEstado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
