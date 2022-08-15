using UrlShortener.Business.Common;
using UrlShortener.Business.DTO.Request;
using UrlShortener.Business.DTO.Response;
using UrlShortener.Business.BusinessException;
using UrlShortener.DataAccess.Repository;

namespace UrlShortener.Business
{
    
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IShortenedUrlRepository _shortenedUrlRepository;
        private readonly IHashService _hashService;
        private readonly IURLService _urlService;

        public UrlShortenerService(IShortenedUrlRepository shortenedUrlRepository, IHashService hashService, IURLService urlService)
        {
            _shortenedUrlRepository = shortenedUrlRepository;
            _hashService = hashService;
            _urlService = urlService;
        }

        public async Task<IEnumerable<ShortenedUrlResponse>> GetAllAsync()
        {
            var data = await _shortenedUrlRepository.GetAllAsync();

            return data.Select(x => new ShortenedUrlResponse()
            {
                Hash = x.Hash,
                Url = x.Url,
            });
        }

        public async Task<ShortenedUrlResponse> GetShortenedUrlAsync(string hash)
        {
            var data = await _shortenedUrlRepository.GetByHashAsync(hash);
            
            if (data is null) throw new URLShortenerBusinessException("URL not found");

            return new ShortenedUrlResponse()
            {
                Hash = data.Hash,
                Url = data.Url,
            };
        }

        private async Task<string> GenerateUniqueHash()
        {
            var hash = _hashService.GenerateHash();
            var data = await _shortenedUrlRepository.GetByHashAsync(hash);
            return (data == null) ? hash : await GenerateUniqueHash();
        }

        public async Task<ShortenedUrlResponse> AddShortenedUrlAsync(ShortenedUrlRequest request)
        {
            if(string.IsNullOrEmpty(request.Url))
                throw new URLShortenerBusinessException("URL not found");
            else if(!_urlService.IsValid(request.Url))
                throw new URLShortenerBusinessException("URL not valid");

            var hash = await GenerateUniqueHash();
            await _shortenedUrlRepository.AddAsync(new Domain.ShortenedUrl() 
            {
                Created = DateTime.Now,
                Hash = hash,
                Url = request.Url,
            });

            return new ShortenedUrlResponse()
            {
                Hash = hash,
                Url = request.Url,
            };
        }
    }
}