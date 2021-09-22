using ApiAssinadora.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Certificado> Certificados { get; set; }
    public DbSet<Documento> Documentos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(m => m.Id).HasMaxLength(110);
            entity.Property(m => m.Email).HasMaxLength(127);
            entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
            entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
            entity.Property(m => m.UserName).HasMaxLength(127);
        });
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(m => m.Id).HasMaxLength(200);
            entity.Property(m => m.Name).HasMaxLength(127);
            entity.Property(m => m.NormalizedName).HasMaxLength(127);
        });
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(m => m.LoginProvider).HasMaxLength(127);
            entity.Property(m => m.ProviderKey).HasMaxLength(127);
        });
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(m => m.UserId).HasMaxLength(127);
            entity.Property(m => m.RoleId).HasMaxLength(127);
        });
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(m => m.UserId).HasMaxLength(110);
            entity.Property(m => m.LoginProvider).HasMaxLength(110);
            entity.Property(m => m.Name).HasMaxLength(110);

        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.Property(x => x.Arquivo).HasColumnType("longblob");
        });

        modelBuilder.Entity<Certificado>(entity =>
       {
           entity.Property(x => x.Arquivo).HasColumnType("longblob");
       });



    }

}