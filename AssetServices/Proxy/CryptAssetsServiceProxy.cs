namespace CryptographyAssets;

using CurrencyBase.Toplist;
using CurrencyBase.Coin;

public class CryptAssetsServiceProxy : ICryptAssetsService
{
  ICryptAssetsService service;
  private Dictionary<string, CoinData>? cashedCurrency;
  private ILogger logger;

  public CryptAssetsServiceProxy(ICryptAssetsService service, ILoggerFactory factory)
  {
    this.service = service;
    logger = factory.CreateLogger("AssetsProxy");
  }

  public async Task<IEnumerable<ToplistCurrencyData>?> GetToplist()
  {
    return await service.GetToplist();
  }

  public async Task<CoinData?> GetCertainAssetAsync(string assetId)
  {
    CoinData? result = null;
    if (cashedCurrency == null)
      cashedCurrency = new Dictionary<string, CoinData>();

    if (cashedCurrency.ContainsKey(assetId))
      result = cashedCurrency[assetId];
    else
    {
      logger.LogInformation("Start load asset");
      result = await service.GetCertainAssetAsync(assetId);
      if (result != null && result.Id != "")
      {
        cashedCurrency.Add(assetId, result);
        logger.LogInformation($"Add to cash: {result.Id}");
      }
      else
        logger.LogWarning($"No such assetId in api: {assetId}");
    }

    return result;
  }
}
