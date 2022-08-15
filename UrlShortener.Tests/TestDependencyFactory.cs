using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.Data;

namespace UrlShortener.Tests
{
    internal static class TestDependencyFactory
    {
        internal static UrlShortenerTestDBContext ConfigureDBContext()
        {
            var options = new DbContextOptionsBuilder<UrlShortenerDBContext>().UseInMemoryDatabase("inMemory").Options;

            var testDBContext = new UrlShortenerTestDBContext(options);
            var db = new UrlShortenerDBContext(options);
            db.Database.EnsureCreated();

            return testDBContext;
        }

        internal static Business.Common.IHashService ConfigureHashService()
        {
            return new Business.Common.HashService();
        }
        internal static Business.Common.IURLService ConfigureURLService()
        {
            return new Business.Common.URLService();
        }
        internal static UrlShortener.DataAccess.Repository.IShortenedUrlRepository ShortenedUrlRepository()
        {
            return new UrlShortener.DataAccess.Repository.EFShortenedUrlRepository(ConfigureDBContext());
        }
        internal static UrlShortener.Business.IUrlShortenerService ConfigureURLShortenerService()
        {
            return new UrlShortener.Business.UrlShortenerService(ShortenedUrlRepository(), ConfigureHashService(), ConfigureURLService());
        }
    }
}
