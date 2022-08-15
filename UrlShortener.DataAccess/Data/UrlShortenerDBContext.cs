using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain;

namespace UrlShortener.DataAccess.Data
{
    public class UrlShortenerDBContext :DbContext
    {
        public UrlShortenerDBContext(DbContextOptions<UrlShortenerDBContext> options) : base(options)
        {

        }

        public DbSet<ShortenedUrl> Urls { get; set; }
    }
}
