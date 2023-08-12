namespace CryptographyAssets;

public class CoinCryptAssetsService : ICryptAssetsService
{
  const string coinApiKey = "241ADC11-8DAD-47CC-A966-40AF4276A633";
  private ILogger logger;
  private HttpClient coinHttpClient;


  public CoinCryptAssetsService(ILoggerFactory loggerFactory)
  {
    logger = loggerFactory.CreateLogger("CoinAssets");
    coinHttpClient = new HttpClient();
  }

  public async Task<IEnumerable<ICurrencyAssetData>?> GetAllAssetsAsync()
  {
    using HttpRequestMessage request = new HttpRequestMessage(
      HttpMethod.Get,
      $"https://rest.coinapi.io/v1/assets"
    );
    request.Headers.Add("X-CoinAPI-Key", coinApiKey);

    logger.LogInformation("Start getting crypt info");
    using var response = await coinHttpClient.SendAsync(request);

    logger.LogInformation("Convert response to collection");
    var json = await response.Content.ReadFromJsonAsync<IEnumerable<CoinJsonData>>();
    if (json == null)
      logger.LogWarning("GetAllAssets return null, try to check url");

    return json?.Select(x => new CoinCurrencyAssetData(x));
  }


  public async Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId)
  {
    logger.LogInformation($"Current asset: {assetId}");

    using HttpRequestMessage request = new HttpRequestMessage(
      HttpMethod.Get,
      $"http://rest.coinapi.io/v1/assets/{assetId}"
    );
    request.Headers.Add("X-CoinAPI-Key", coinApiKey);

    logger.LogInformation("Start getting crypt info");
    using var response = await coinHttpClient.SendAsync(request);

    logger.LogInformation("Convert response to CurrencyData");
    ICurrencyAssetData? result = null;
    try
    {
      var responseContent = await response.Content.ReadFromJsonAsync<List<CoinJsonData>>();

      if (responseContent == null || responseContent.Count == 0)
        logger.LogWarning("GetAllAssets return null, try to check url");
      else
      {
        result = new CoinCurrencyAssetData(responseContent[0]);
        if(result.PriceUsd == 0) //its decimal so ok
          result.PriceUsd = 1;
      }
    }
    catch
    {
      string responseError = await response.Content.ReadAsStringAsync();
      logger.LogCritical(responseError); 
    }
    return result;
  }  
  private record class ErrorMessage(string error);
}
