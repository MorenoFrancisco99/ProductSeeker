using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Context
{
    public class AplicationDBContext : IdentityDbContext<AppUser>
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) 
            :base(options)
        { 
        }

        // public DbSet<ProductSeeker.Data.OldModels.ProductModel> Products { get; set; }
        // public DbSet<ProductSeeker.Data.OldModels.StoreModel> Stores { get; set; }
        // public DbSet<ProductSeeker.Data.OldModels.AppUserProduct> AppUserProducts { get; set; }
        // public DbSet<ProductSeeker.Data.OldModels.AppUserStore> AppUserStores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Itera por las entidades del modelbuilder y asigna
            //el valor por defecto a CreationDate que sera asignado en el DB
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if(typeof(BaseEntity).IsAssignableFrom(entity.ClrType))
                {
                    builder.Entity(entity.ClrType)
                        .Property(nameof(BaseEntity.CreationDate))
                        .HasDefaultValueSql("SYSUTCDATETIME()")
                        .ValueGeneratedOnAdd();
                }
            }
            //AppUser ya hereda otra clase asi que se asigna manualmente el valor
            builder.Entity<AppUser>()
                .Property(u => u.CreationDate)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .ValueGeneratedOnAdd();
   
            
            // A partir de .NET9 se usa .UseSeeding(). Revisar para un futuro
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                     Id = "ADMIN_ROLE_ID", 
                     Name = "Admin",
                     NormalizedName = "ADMIN",
                     ConcurrencyStamp = "STATIC_ADMIN_STAMP" 
                },
                new IdentityRole
                {
                    Id = "USER_ROLE_ID", 
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "STATIC_USER_STAMP" 
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            
            


            // builder.Entity<AppUserStore>()
            //     .HasKey(x => new { x.AppUserId, x.StoreId });
            // builder.Entity<AppUserStore>()
            //     .HasOne(x => x.AppUser)
            //     .WithMany(x => x.AppUserStores)
            //     .HasForeignKey(x => x.AppUserId);
            // builder.Entity<AppUserStore>()
            //     .HasOne(x => x.StoreModel)
            //     .WithMany(x => x.AppUserStores)
            //     .HasForeignKey(x => x.StoreId);
            //
            //
            // builder.Entity<AppUserProduct>()
            //     .HasKey(x => new { x.AppUserId, x.ProductId });
            // builder.Entity<AppUserProduct>()
            //     .HasOne(x => x.AppUser)
            //     .WithMany(x => x.AppUserProducts)
            //     .HasForeignKey(x => x.AppUserId);
            // builder.Entity<AppUserProduct>()
            //     .HasOne(x => x.ProductModel)
            //     .WithMany(x => x.AppUserProducts)
            //     .HasForeignKey(x => x.ProductId);
        }

    }
}
