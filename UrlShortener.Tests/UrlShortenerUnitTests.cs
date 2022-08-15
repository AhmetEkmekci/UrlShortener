namespace UrlShortener.Tests
{
    [TestClass]
    public class UrlShortenerUnitTests
    {
        [TestMethod]
        [DataRow("http://google.com")]
        [DataRow("https://google.com")]
        public void URLShuldCreatedAndRetrieved(string url)
        {
            var service = TestDependencyFactory.ConfigureURLShortenerService();

            var CreatedResult = service.AddShortenedUrlAsync(new Business.DTO.Request.ShortenedUrlRequest() { Url = url }).Result;
            var RetrievedResult = service.GetShortenedUrlAsync(CreatedResult.Hash).Result;

            Assert.IsNotNull(CreatedResult);
            Assert.IsNotNull(CreatedResult.Hash);
            Assert.IsNotNull(RetrievedResult.Url);
        }

        [TestMethod]
        [DataRow("_http://google.com")]
        [DataRow(null)]
        public void URLShuldThrowValidateOrEmptyException(string url)
        {
            var service = TestDependencyFactory.ConfigureURLShortenerService();

            Assert.ThrowsException<UrlShortener.Business.BusinessException.URLShortenerBusinessException>(() =>
            {
                throw service.AddShortenedUrlAsync(new Business.DTO.Request.ShortenedUrlRequest() { Url = url }).Exception.Flatten().InnerException;
            });
        }

        [TestMethod]
        [DataRow("tst")]
        public void URLShuldThrowNotFoundException(string hash)
        {
            var service = TestDependencyFactory.ConfigureURLShortenerService();

            Assert.ThrowsException<UrlShortener.Business.BusinessException.URLShortenerBusinessException>(() =>
            {
                throw service.GetShortenedUrlAsync(hash).Exception.Flatten().InnerException;
            });
        }
    }
}