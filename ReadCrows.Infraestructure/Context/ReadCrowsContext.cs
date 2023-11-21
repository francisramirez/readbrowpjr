 
using Microsoft.EntityFrameworkCore;
using ReadCrows.Core.Entities;

namespace ReadCrows.Infraestructure.Context;

public partial class ReadCrowsContext : DbContext
{
    public ReadCrowsContext(DbContextOptions<ReadCrowsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07BB60ACA8");

            entity.Property(e => e.Correo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}