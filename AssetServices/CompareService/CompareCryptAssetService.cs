namespace CryptographyAssets.CompareService;

using CurrencyBase.Toplist;
using System.Text.Json;

public class CompareCryptAssetsService : ICryptAssetsService
{
  const string apiKey = "23a9aa16552ff83490e5669dfaa9feb686badc250919411d1a4211f9b0a7a03a";
  private ILogger logger;
  private HttpClient httpClient;
  private Dictionary<string, string> urlIcons = new Dictionary<string, string>();
  private string _iconsPath = @"AssetServices/CompareService/iconUrls.txt";

  public CompareCryptAssetsService(ILoggerFactory factory)
  {
    logger = factory.CreateLogger("CompareAssets");
    httpClient = new HttpClient();
    FillUrlDictionary(urlIcons);
  }

  private async void FillUrlDictionary(Dictionary<string, string> dict)
  {
    using(StreamReader reader = new StreamReader(_iconsPath))
    {
      string? temp;
      while((temp = await reader.ReadLineAsync()) != null)
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
        result = json.Data.Select(x => {
            var res = new CompareToplistCurrencyData(x); 

            if(urlIcons.ContainsKey(res.Id))
              res.SetIconUrl(urlIcons[res.Id]);
            else
              Task.Run(async() => await AddIconToDatabase(urlIcons, res));
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

  private async Task AddIconToDatabase(Dictionary<string,string> dictionary, CompareToplistCurrencyData data)
  {
    string id = data.Id;
    HttpResponseMessage response = await httpClient.GetAsync($"https://data-api.cryptocompare.com/asset/v1/data/by/symbol?asset_symbol={id.ToUpper()}&api_key={apiKey}");
    var json = await response.Content.ReadFromJsonAsync<DeserializedIcon>();
    if(json != null)
    {
      string url = json.Data.Logo_Url;
      dictionary.TryAdd(id, url);
      using(StreamWriter writer = new StreamWriter(_iconsPath, true)) 
      {
        string newLine = $"{id} {url}";
        await writer.WriteLineAsync(newLine);
      }
      data.SetIconUrl(url);
    }
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
        result = new CurrencyAssetData(json);
    }
    catch (JsonException ex)
    {
      logger.LogWarning($"Unable to deserialize object because user is ****\n{ex.Message}");
    }
    return result;
  }
}
