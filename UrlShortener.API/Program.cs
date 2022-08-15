using UrlShortener.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder, builder.Configuration);

#region Minimal API

var app = builder.Build();

app.UrlShortenerBusinessExceptionHandler();

app.MapGet("/", async () =>
{
    return Results.Json(new { CreateURL = "/Create?url=[url]" });
});
app.MapGet("/Create", async (string url) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var urlShortenerService = scope.ServiceProvider.GetService<UrlShortener.Business.IUrlShortenerService>();
        var result = await urlShortenerService.AddShortenedUrlAsync(new UrlShortener.Business.DTO.Request.ShortenedUrlRequest() { Url = url });
        return Results.Json(new { ShortenedUrl = $"{app.Urls.First()}/{result.Hash}" });
    }
});
app.MapGet("/{hash}", async (string hash) =>
{
    var cacheService = app.Services.GetService<UrlShortener.Infrastructure.Services.IShortenedUrlCacheService>();
    using (var scope = app.Services.CreateScope())
    {
        if (!cacheService.UrlStore.TryGetValue(hash, out var url))
        {
            var urlShortenerService = scope.ServiceProvider.GetService<UrlShortener.Business.IUrlShortenerService>();
            var result = await urlShortenerService.GetShortenedUrlAsync(hash);
            if (result is not null)
            {
                url = result?.Url;
                cacheService.UrlStore.TryAdd(hash, url);
            }
        }

        if (url is not null)
            return Results.Redirect(url, true);
        else
            return Results.NotFound();
    }
});

app.Run();

#endregion




