using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Data.Context
{
    public class AplicationDBContext : IdentityDbContext<AppUser>
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCoreModel> ProductCores { get; set; }
        public DbSet<ProductSpecModel> ProductSpecs { get; set; }
        public DbSet<AppUserProductPriceModel> AppUserProductPrices { get; set; }
        public DbSet<ProductAliasModel> ProductAliasModel { get; set; }
        public DbSet<StoreCoreModel> StoreCores { get; set; }
        public DbSet<StoreSpecModel> StoreSpecs { get; set; }
        public DbSet<AppUserStoreCoreModel> AppUserStoreCores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Itera por las entidades del modelbuilder y asigna
            //el valor por defecto a CreationDate que sera asignado en el DB
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType))
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


            //Disable cascade delete on all entities
            foreach (var relationship in builder.Model
                                     .GetEntityTypes()
                                     .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }



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

            builder.Entity<ProductSpecModel>()
            .HasDiscriminator(p => p.Category)
            .HasValue<FoodProductModel>(CategoriesEnum.ProductCategories.Food);


        }

    }
}
