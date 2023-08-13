namespace CryptographyAssets.CompareService;
using System.Text.Json;
public class CompareCryptAssetsService : ICryptAssetsService
{
  const string apiKey = "23a9aa16552ff83490e5669dfaa9feb686badc250919411d1a4211f9b0a7a03a";
  private ILogger logger;
  private HttpClient httpClient;

  public CompareCryptAssetsService(ILoggerFactory factory)
  {
    logger = factory.CreateLogger("CompareAssets");
    httpClient = new HttpClient();
  }

  public async Task<IEnumerable<ICurrencyAssetData>?> GetToplist()
  {
    using HttpResponseMessage response = await httpClient.GetAsync(
      $"https://min-api.cryptocompare.com/data/top/totalvolfull?limit=90&tsym=USD&api_key={apiKey}"
    );
    
    IEnumerable<ICurrencyAssetData>? result = null;
    try
    {
      var json = await response.Content.ReadFromJsonAsync<DeserializedToplist>();
      if(json != null && json.Data != null)
      {
        result = json.Data.Select(x => new ListCurrencyAssetData(x));
      }
    }
    catch(JsonException ex)
    {
      logger.LogWarning($"Unable to deserialize object because api is ****\n{ex.Message}");
    }
    return result;
  }

  public async Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId)
  {
    logger.LogInformation($"Current asset: {assetId}");
    HttpResponseMessage response = await httpClient.GetAsync(
      $"https://min-api.cryptocompare.com/data/top/exchanges?fsym={assetId}&tsym=USD&api_key={apiKey}"
    );

    ICurrencyAssetData? result = null;
    try
    {
    var json = await response.Content.ReadFromJsonAsync<DeserializedVolumeArray>();
    if (json != null)
      result = new VolumeCurrencyAssetData(json);
    }
    catch(JsonException ex)
    {
      logger.LogWarning($"Unable to deserialize object because user is ****\n{ex.Message}");
    }
    return result;
  }
}



