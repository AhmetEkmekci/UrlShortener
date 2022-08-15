using UrlShortener.Domain;

namespace UrlShortener.DataAccess.Repository
{
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {
        Task<ShortenedUrl> GetByHashAsync(string hash);
    }
}