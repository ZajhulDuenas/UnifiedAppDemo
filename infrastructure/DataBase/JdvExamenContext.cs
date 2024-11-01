using System;
using System.Collections.Generic;
using infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.DataBase;

public partial class JdvExamenContext : DbContext
{
    public JdvExamenContext()
    {
    }

    public JdvExamenContext(DbContextOptions<JdvExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatRol> CatRols { get; set; }

    public virtual DbSet<CatUsuario> CatUsuarios { get; set; }

    public virtual DbSet<TblEmpleado> TblEmpleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<CatRol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Cat_Rol");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<CatUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Cat_Usuario");

            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(10);
            entity.Property(e => e.Usuario).HasMaxLength(10);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.CatUsuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Cat_Usuario_Cat_Rol");
        });

        modelBuilder.Entity<TblEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("tbl_Empleado");

            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
