namespace CryptographyAssets;
public class CryptAssetsServiceProxy: ICryptAssetsService
{
  ICryptAssetsService service;
  private Dictionary<string, ICurrencyAssetData>? cashedCurrency;
  private ILogger logger;

  public CryptAssetsServiceProxy(ICryptAssetsService service, ILoggerFactory factory)
  {
    this.service = service;
    logger = factory.CreateLogger("AssetsProxy");
  }

  public async Task<IEnumerable<ICurrencyAssetData>?> GetAllAssetsAsync()
  {
    return await service.GetAllAssetsAsync();
  }

  public async Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId)
  {
    ICurrencyAssetData? result = null;
    if(cashedCurrency == null)
      cashedCurrency = new Dictionary<string, ICurrencyAssetData>();
    else if(cashedCurrency.ContainsKey(assetId))
      result = cashedCurrency[assetId];
    else 
    {
      result = await service.GetCertainAssetAsync(assetId);
      if(result != null && result.AssetId!= "")
      {
        cashedCurrency.Add(assetId, result);
        logger.LogInformation($"Add to cash: {result.AssetId}");
      }
      else
        logger.LogWarning($"No such assetId in api: {assetId}");
    }

    return result;
  }
}

