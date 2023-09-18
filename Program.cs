using Extensions.CryptService;
using CryptoCurrencyService.Compare;
using CryptoCurrencyService.Proxy;
using Geo;

var builder = WebApplication.CreateBuilder();

builder.Services.AddMemoryCache();

builder.Services.AddCryptService<CompareCryptAssetsService, CryptAssetsServiceProxy>();
builder.Services.AddSingleton<IGeoService, GeoapifyService>();

var app = builder.Build();

app.UseFileServer();

app.MapPost(
  "/country",
  async (HttpContext context, IGeoService geo) =>
  {
    return await geo.Init(context.Request);
  }
);

app.MapGet("/crypt", GetToplist);
app.MapGet("/crypt/mobile", GetToplist); //for a moment poher, I dont see the difference

app.MapGet(
  "/coins/{assetId}",
  async (HttpContext context, string assetId, ILogger<Program> logger) =>
  {
    var cryptService = app.Services.GetCryptService<CryptAssetsServiceProxy>()!;
    logger.LogInformation($"Go to: {assetId}");

    var assetInfo = await cryptService.GetCertainAssetAsync(assetId.ToUpper());
    await context.Response.WriteAsJsonAsync(assetInfo);
  }
);

app.MapGet(
  "/",
  async (HttpContext context, IGeoService geo, ILogger<Program> logger) => {
    context.Request.Cookies.TryGetValue("currency", out var currency);
    if(currency != null)
      geo.Currency = currency;
    logger.LogInformation($"GEO Getting from start: {geo.Currency}");
    await context.Response.SendFileAsync(@"wwwroot/html/toplist.html");
  }
);

app.MapGet(
  "/coin",
  async (context) => await context.Response.SendFileAsync(@"wwwroot/html/coin.html")
);

app.Run();

async Task GetToplist(HttpContext context, CryptAssetsServiceProxy cryptService)
{
  var assets = await cryptService.GetToplist();
  await context.Response.WriteAsJsonAsync(assets);
}

