namespace UrlShortener.Business.Common
{
    public interface IURLService
    {
        public bool IsValid(string url);
    }
}