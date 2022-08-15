using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Business.DTO.Request;
using UrlShortener.Business.DTO.Response;

namespace UrlShortener.Business
{
    public interface IUrlShortenerService
    {
        public Task<IEnumerable<ShortenedUrlResponse>> GetAllAsync();
        public Task<ShortenedUrlResponse> GetShortenedUrlAsync(string hash);
        public Task<ShortenedUrlResponse> AddShortenedUrlAsync(ShortenedUrlRequest request);
    }
}
