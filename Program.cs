using CryptographyAssets;
using CryptographyAssets.CompareService;
using CryptographyAssets.Mobile;
using Geo;

var builder = WebApplication.CreateBuilder();

builder.Services.AddAssetsService<CompareCryptAssetsService, CryptAssetsServiceProxy>();
builder.Services.AddAssetsMobileService<CryptAssetsServiceMobile>();
builder.Services.AddSingleton<IGeoService, GeoapifyService>();

var app = builder.Build();

app.UseFileServer();

app.MapPost(
  "/country",
  async (HttpContext context, IGeoService geo) =>
  {
    return await geo.GetData(context.Request);
  }
);

app.MapGet("/crypt", GetToplist);
app.MapGet("/crypt/mobile", GetToplist); //for a moment poher, I dont see the difference

app.MapGet(
  "/coins/{assetId}",
  async (HttpContext context, string assetId, ILogger<Program> logger) =>
  {
    var cryptService = app.Services.GetAssetsService<ICryptAssetsService>()!;
    logger.LogInformation($"Go to: {assetId}");

    var assetInfo = await cryptService.GetCertainAssetAsync(assetId.ToUpper());
    await context.Response.WriteAsJsonAsync(assetInfo);
  }
);

app.MapGet(
  "/",
  async (context) => await context.Response.SendFileAsync(@"wwwroot/html/toplist.html")
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

async Task GetMobileToplist(HttpContext context, CryptAssetsServiceMobile cryptMobileService)
{
  var assets = cryptMobileService.GetToplist();
  await context.Response.WriteAsJsonAsync(assets);
}
