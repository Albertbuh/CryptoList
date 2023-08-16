using CryptographyAssets;
using CryptographyAssets.CompareService;
using CryptographyAssets.Mobile;

var builder = WebApplication.CreateBuilder();

builder.Services.AddAssetsService<CompareCryptAssetsService, CryptAssetsServiceProxy>();
builder.Services.AddAssetsMobileService<CryptAssetsServiceMobile>();

var app = builder.Build();

app.UseFileServer();

app.MapGet("/crypt", GetToplist);
app.MapGet("/crypt/mobile", GetToplist); //for a moment poher, I dont see the difference

app.MapGet(
  "/crypt/coins/{assetId}",
  async (HttpContext context, string assetId, ILogger<Program> logger) =>
  {
    var cryptService = app.Services.GetAssetsService<CryptAssetsServiceProxy>()!;
    logger.LogInformation($"Go to: {assetId}");
    
    var assetInfo = await cryptService.GetCertainAssetAsync(assetId.ToUpper());
    await context.Response.WriteAsJsonAsync(assetInfo);
  }
);

app.MapGet(
  "/",
  async (context) => await context.Response.SendFileAsync(@"wwwroot/html/toplist.html")
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


