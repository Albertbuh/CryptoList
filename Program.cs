using CryptographyAssets;
using CryptographyAssets.CompareService;

var builder = WebApplication.CreateBuilder();

builder.Services.AddAssetsService<CompareCryptAssetsService, CryptAssetsServiceProxy>();

var app = builder.Build();

app.UseFileServer();

app.MapGet(
  "/crypt",
  async (HttpContext context) =>
  {
    var cryptService = app.Services.GetAssetsService<CryptAssetsServiceProxy>()!;
    var assets = await cryptService.GetToplist();
    await context.Response.WriteAsJsonAsync(assets);
  }
);

app.MapGet(
  "/crypt/{assetId}",
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
