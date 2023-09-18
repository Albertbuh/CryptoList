namespace CryptoCurrencyService.Proxy;

using Microsoft.Extensions.Caching.Memory;

public class CryptAssetsServiceProxy : ICryptAssetsService
{
  ICryptAssetsService cryptService;
  private IMemoryCache cache;
  private ILogger logger;

  public CryptAssetsServiceProxy(
    ICryptAssetsService cryptService,
    IMemoryCache cache,
    ILoggerFactory factory
  )
  {
    this.cryptService = cryptService;
    this.cache = cache;
    logger = factory.CreateLogger("AssetsProxy");
  }

  public async Task<IEnumerable<ToplistCurrencyData>?> GetToplist()
  {
    return await cryptService.GetToplist();
  }

  public async Task<CoinData?> GetCertainAssetAsync(string assetId)
  {
    cache.TryGetValue(assetId, out CoinData? result);

    if (result == null)
    {
      logger.LogInformation($"Start load asset: {assetId}");
      result = await cryptService.GetCertainAssetAsync(assetId);

      if (result != null && result.Id != "")
      {
        cache.Set(
          assetId,
          result,
          new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
        );
        logger.LogInformation($"Add to cash: {result.Id}");
      }
      else
        logger.LogWarning($"No such assetId in api: {assetId}");
    }
    else
    {
      logger.LogInformation($"Loaded from cache {assetId}");
    }

    return result;
  }
}
