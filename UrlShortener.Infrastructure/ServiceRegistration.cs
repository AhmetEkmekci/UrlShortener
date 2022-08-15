using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UrlShortener.DataAccess.Data;
using UrlShortener.DataAccess.Repository;
using UrlShortener.Business;
using UrlShortener.Business.Common;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
        {
            #region Log
            services.AddSingleton(typeof(ILogService), typeof(LogService));
            #endregion

            #region Cache
            services.AddSingleton<IShortenedUrlCacheService, ShortenedUrlCacheService>();
            #endregion


            var connectionString = builder.Configuration.GetConnectionString("db");
            var host = builder.Configuration.GetValue<string>("DefaultSqlHost");
            connectionString = connectionString.Replace("[HOST]", host);
            
            builder.Services.AddDbContext<UrlShortenerDBContext>(conf => conf.UseSqlServer(connectionString, db => db.MigrationsAssembly("UrlShortener.DataAccess")));
            builder.Services.AddScoped<IShortenedUrlRepository, EFShortenedUrlRepository>();
            builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
            builder.Services.AddScoped<IHashService, HashService>();
            builder.Services.AddScoped<IURLService, URLService>();

            builder.Services.AddResponseCaching();
            builder.Services.AddMemoryCache();

        }
    }
}
