using Microsoft.EntityFrameworkCore;

namespace Api.Models.DataBase;

public partial class JvfcontrolContext : DbContext
{
    public JvfcontrolContext()
    {
    }

    public JvfcontrolContext(DbContextOptions<JvfcontrolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<AppUserRole> AppUserRoles { get; set; }

    public virtual DbSet<BusinessUnit> BusinessUnits { get; set; }

    public virtual DbSet<BusinessUnitAppUser> BusinessUnitAppUsers { get; set; }

    public virtual DbSet<CatRol> CatRols { get; set; }

    public virtual DbSet<CatUsuario> CatUsuarios { get; set; }

    public virtual DbSet<CecoCtasCont> CecoCtasConts { get; set; }

    public virtual DbSet<TblEmpleado> TblEmpleados { get; set; }

    public virtual DbSet<TransferRequest> TransferRequests { get; set; }

    public virtual DbSet<TransferRequestSource> TransferRequestSources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.IdAppUser).HasName("PK_APPUSER");

            entity.ToTable("AppUser");

            entity.Property(e => e.EmailUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AppUserRole>(entity =>
        {
            entity.HasKey(e => e.IdAppUserRole).HasName("PK_APPUSERROLE");

            entity.ToTable("AppUserRole");

            entity.Property(e => e.IdAppUserRole).ValueGeneratedNever();
            entity.Property(e => e.NameUserRole)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BusinessUnit>(entity =>
        {
            entity.HasKey(e => e.IdBusinessUnit).HasName("PK_BUSINESSUNIT");

            entity.ToTable("BusinessUnit");
        });

        modelBuilder.Entity<BusinessUnitAppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BUSINESSUNITAPPUSER");

            entity.ToTable("BusinessUnitAppUser");

            entity.Property(e => e.TopAmount).HasColumnType("money");

            entity.HasOne(d => d.IdAppUserNavigation).WithMany(p => p.BusinessUnitAppUsers)
                .HasForeignKey(d => d.IdAppUser)
                .HasConstraintName("FK_BUSINESSAPUSR_APPUSER");

            entity.HasOne(d => d.IdAppUserRoleNavigation).WithMany(p => p.BusinessUnitAppUsers)
                .HasForeignKey(d => d.IdAppUserRole)
                .HasConstraintName("FK_BUSINESSAPUSR_APPUSERR");

            entity.HasOne(d => d.IdBusinessUnitNavigation).WithMany(p => p.BusinessUnitAppUsers)
                .HasForeignKey(d => d.IdBusinessUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BUSINESSAPUSR_BUSINESSUN");
        });

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

        modelBuilder.Entity<CecoCtasCont>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CECOCTASCONT");

            entity.ToTable("CecoCtasCont");

            entity.Property(e => e.CodeCeco)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CodeCtasCont)
                .HasMaxLength(15)
                .IsUnicode(false);
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

        modelBuilder.Entity<TransferRequest>(entity =>
        {
            entity.HasKey(e => e.IdRequest).HasName("PK_TRANSFERREQUEST");

            entity.ToTable("TransferRequest");

            entity.Property(e => e.InAmount).HasColumnType("money");
            entity.Property(e => e.Justification).HasColumnType("text");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.IdCtaContDestinationNavigation).WithMany(p => p.TransferRequests)
                .HasForeignKey(d => d.IdCtaContDestination)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERRE_CECOCTAS");

            entity.HasOne(d => d.IdRequesterNavigation).WithMany(p => p.TransferRequestIdRequesterNavigations)
                .HasForeignKey(d => d.IdRequester)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERRE_RE_APPUSER");

            entity.HasOne(d => d.IdUserExecutorNavigation).WithMany(p => p.TransferRequestIdUserExecutorNavigations)
                .HasForeignKey(d => d.IdUserExecutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERRE_EX_APPUSER");
        });

        modelBuilder.Entity<TransferRequestSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TRANSFERREQUESTSOURCE");

            entity.ToTable("TransferRequestSource");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.OutAmount).HasColumnType("money");
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TransferRequestSource)
                .HasForeignKey<TransferRequestSource>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERSO_CECOCTAS");

            entity.HasOne(d => d.IdRequestNavigation).WithMany(p => p.TransferRequestSources)
                .HasForeignKey(d => d.IdRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERSO_TRANSFERRE");

            entity.HasOne(d => d.IdUserApproverNavigation).WithMany(p => p.TransferRequestSources)
                .HasForeignKey(d => d.IdUserApprover)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSFERSO_APPUSER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
