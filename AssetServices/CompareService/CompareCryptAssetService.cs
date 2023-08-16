namespace CryptographyAssets.CompareService;

using System.Text.Json;

using CurrencyBase.Toplist;
using CurrencyBase.Coin;
using Geo;

public class CompareCryptAssetsService : ICryptAssetsService
{
  const string apiKey = "23a9aa16552ff83490e5669dfaa9feb686badc250919411d1a4211f9b0a7a03a";
  private ILogger logger;
  private IGeoService geo;
  private HttpClient httpClient;
  private Dictionary<string, string> urlIcons = new Dictionary<string, string>();
  private string _iconsPath = @"AssetServices/CompareService/iconUrls.txt";

  public CompareCryptAssetsService(ILoggerFactory factory, IGeoService geo)
  {
    logger = factory.CreateLogger("CompareAssets");
    this.geo = geo;

    var socketsHandler = new SocketsHttpHandler
    {
      PooledConnectionLifetime = TimeSpan.FromSeconds(3.5)
    };
    httpClient = new HttpClient(socketsHandler);

    FillUrlDictionary(urlIcons);
  }

  private async void FillUrlDictionary(Dictionary<string, string> dict)
  {
    using (StreamReader reader = new StreamReader(_iconsPath))
    {
      string? temp;
      while ((temp = await reader.ReadLineAsync()) != null)
      {
        string[] url = temp.Split(' ');
        dict.TryAdd(url[0], url[1]);
      }
    }
    logger.LogInformation("Urls are loaded");
  }

  public async Task<IEnumerable<ToplistCurrencyData>?> GetToplist()
  {
    using HttpResponseMessage response = await httpClient.GetAsync(
      $"https://min-api.cryptocompare.com/data/top/totalvolfull?limit=100&tsym=USD&api_key={apiKey}"
    );

    IEnumerable<CompareToplistCurrencyData>? result = null;
    try
    {
      var json = await response.Content.ReadFromJsonAsync<DeserializedToplist>();
      if (json != null && json.Data != null)
      {
        result = json.Data.Select(x =>
        {
          var res = new CompareToplistCurrencyData(x);

          if (urlIcons.ContainsKey(res.Id))
            res.SetIconUrl(urlIcons[res.Id]);
          else
            Task.Run(async () => await AddIconToDatabase(urlIcons, res));
          return res;
        });
      }
    }
    catch (JsonException ex)
    {
      logger.LogWarning($"Unable to deserialize object because api is ****\n{ex.Message}");
    }
    return result;
  }

  private async Task AddIconToDatabase(
    Dictionary<string, string> dictionary,
    CompareToplistCurrencyData data
  )
  {
    string id = data.Id;
    HttpResponseMessage response = await httpClient.GetAsync(
      $"https://data-api.cryptocompare.com/asset/v1/data/by/symbol?asset_symbol={id.ToUpper()}&api_key={apiKey}"
    );
    var json = await response.Content.ReadFromJsonAsync<DeserializedIcon>();
    if (json != null)
    {
      string url = json.Data.Logo_Url;
      dictionary.TryAdd(id, url);
      using (StreamWriter writer = new StreamWriter(_iconsPath, true))
      {
        string newLine = $"{id} {url}";
        await writer.WriteLineAsync(newLine);
      }
      data.SetIconUrl(url);
    }
  }

  protected async Task<CompareCoinData> GetPrices(CompareCoinData coinData, string assetId)
  {
    logger.LogInformation($"GEO: {geo.Currency}");
    string userCurrency = "JPY";
    if (geo.Currency != "" && geo.Currency != "USD" && geo.Currency != "EUR")
      userCurrency = geo.Currency;

    HttpResponseMessage response = await httpClient.GetAsync(
      $"https://min-api.cryptocompare.com/data/price?fsym={assetId}&tsyms=USD,EUR,{userCurrency}&api_key={apiKey}"
    );

    coinData.SetPrices(await response.Content.ReadAsStringAsync());
    return coinData;
  }

  public async Task<CoinData?> GetCertainAssetAsync(string assetId)
  {
    assetId = assetId.ToUpper();
    logger.LogInformation($"Current asset: {assetId}");
    CompareCoinData? result = null;
    HttpResponseMessage response = await httpClient.GetAsync($"https://data-api.cryptocompare.com/asset/v1/data/by/symbol?asset_symbol={assetId}&api_key={apiKey}");
    try 
    { 
      var json = await response.Content.ReadFromJsonAsync<DeserializedCoinData>();
      if(json != null)
      {
        result = new CompareCoinData(json);
        result = await GetPrices(result, assetId);
      }
    }
    catch (JsonException ex)
    {
      logger.LogWarning($"Unable to deserialize object because user is ****\n{ex.Message}");
    }
    return result;
  }
}
