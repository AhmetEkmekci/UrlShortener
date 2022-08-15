namespace UrlShortener.Domain
{
    public class ShortenedUrl : IEntity
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
}