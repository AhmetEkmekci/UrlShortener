using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Infrastructure.Services
{
    public class ShortenedUrlCacheService : IShortenedUrlCacheService
    {
        private ConcurrentDictionary<string, string> urlStore;
        public ConcurrentDictionary<string, string> UrlStore
        {
            get
            {
                if (urlStore is null)
                {
                    urlStore = new ConcurrentDictionary<string, string>();
                }
                return urlStore;
            }
            set
            {
                urlStore = value;
            }
        }
    }

    public interface IShortenedUrlCacheService
    {
        ConcurrentDictionary<string, string> UrlStore { get; set; }
    }
}
