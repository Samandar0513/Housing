using BizLayer.Services.Implementations;
using BizLayer.Services.Interfaces;
using BizLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLayer.DTOs.Comman;
using Microsoft.Extensions.Options;
using Minio;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BizLayer
{
    public static class BizLayerDependencyInjection
    {
        public static IServiceCollection AddBizLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddJwtAuthentication(configuration);
            services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
            services.Configure<JwtOption>(configuration.GetSection("JwtOption"));
            return services;
        }
        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<Helper>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

            //services.AddHostedService<RabbitMQConsumer>();

            //Minio sozlamalari
            services.AddScoped<IFileStorageService, MinioFileStorageService>();

            services.AddSingleton<IMinioClient>(sp =>
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
        }
        private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // JwtOption'ni o'qish
            var jwtOption = new JwtOption();
            configuration.GetSection("JwtOption").Bind(jwtOption);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOption.Key)
                    ),

                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
