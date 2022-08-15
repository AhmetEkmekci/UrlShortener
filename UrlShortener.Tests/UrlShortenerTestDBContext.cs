using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess.Data;
using UrlShortener.Domain;

namespace UrlShortener.Tests
{
    internal class UrlShortenerTestDBContext : UrlShortenerDBContext
    {
        public UrlShortenerTestDBContext(DbContextOptions<UrlShortenerDBContext> options) : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            var data = @"[
{Id: 1, Hash:""test"", Url:""http://yahoo.com""},
]";
            base.OnModelCreating(modelBuilder);
            seedDataWithJsonFile<UrlShortener.Domain.ShortenedUrl>(modelBuilder, data);
        }

        void seedDataWithJsonFile<T>(ModelBuilder modelBuilder, string json) where T : class, IEntity
        {
            var data = JsonConvert.DeserializeObject<List<T>>(json);
            modelBuilder.Entity<T>().HasData(data);
        }
    }
}
