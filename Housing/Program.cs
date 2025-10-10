
using Housing.Context;
using Housing.Models.DTOs.Comman;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;

namespace Housing
{
    public class Program
    {
        //asdsa
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Add services to the container.
            //DI Container
            builder.Services.AddScoped<Services.Helper>();
            builder.Services.AddScoped<Services.Interfaces.IJwtTokenService, Services.JwtTokenService>();
            builder.Services.AddScoped<Services.Interfaces.IAuthService, Services.AuthService>();
            builder.Services.AddScoped<Services.Interfaces.IGeneralService, Services.GeneralService>();
            builder.Services.AddScoped<Services.Interfaces.IPropertyService, Services.PropertyService>();

            //Minio sozlamalari
            builder.Services.AddScoped<Services.Interfaces.IFileStorageService, Services.MinioFileStorageService>();

            builder.Services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
            builder.Services.AddSingleton<IMinioClient>(sp =>
            {
                var minioSettings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

                // MinioClient obyektini yaratish
                var client = new MinioClient()
                    .WithEndpoint(minioSettings.Endpoint)
                    .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey);

                // Agar SSL yoqilgan bo'lsa
                if (minioSettings.UseSsl)
                {
                    client = client.WithSSL();
                }

                return client.Build(); // MinioClient ni qurish
            });


            //JWT Konfiguratsiyasi
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                    )
                };
            });
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            //Swagger uchun JWT sozlamalari
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your_Sln", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Authentication va Authorization o'rnatish
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
