using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Data.Repositories;
using ProductSeeker.Data.Services;
using NetTopologySuite.Geometries;

namespace ProductSeeker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AplicationDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("LinuxString") ?? throw new InvalidOperationException("Connection string 'ProductsDBContext' not found"),
                     x => x.UseNetTopologySuite()));
           
           
            builder.Services.AddCors(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173"
                        )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //Configuracion de la password
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<AplicationDBContext>();



            builder.Services.AddAuthentication(options =>
            {
                //Aparentemente esto aveces se deja en blaco para dejarlo por defecto
                //Se agrega todo esto para dejarlo explicitamente pero pareciera ser la config default
                //Esto parece ser solo lo del JWT pero se pueden configurar Cookies aca 
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Agregar ValidateLifetime
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
                };
            });



            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters
                        .Add(new JsonStringEnumConverter());

                    options.JsonSerializerOptions.UnmappedMemberHandling =
                        JsonUnmappedMemberHandling.Disallow;
                    //Esto hace que si el cliente manda un campo que no existe en el DTO, 
                    // el deserializador va a tirar error en vez de ignorarlo, 
                    // lo cual es util para evitar errores silenciosos por typos o 
                    // cambios en el DTO que el cliente no actualizo.
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Genera codigo una casilla en swagger para realizar la auth de manera automatica y no tener que hacrlo todo el tiempo
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            //DI config
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddValidatorsFromAssemblyContaining<FoodValidator>();



            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var adminUsername = builder.Configuration["AdminSeed:Username"]!;
                var adminEmail = builder.Configuration["AdminSeed:Email"]!;
                var adminPassword = builder.Configuration["AdminSeed:Password"]!;

                var existing = await userManager.FindByNameAsync(adminUsername);
                if (existing == null)
                {
                    var adminUser = new AppUser
                    {
                        UserName = adminUsername,
                        Email = adminEmail,
                        GeoLocation = null,
                        IsActive = true,
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    else
                        Console.WriteLine($"Error creando admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();


            app.UseCors("AllowAllOrigins");
            //Si estos estan intercambiados no funciona XD
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
