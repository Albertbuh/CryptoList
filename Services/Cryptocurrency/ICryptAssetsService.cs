namespace CryptoCurrencyService;

public interface ICryptAssetsService
{
  public Task<IEnumerable<ToplistCurrencyData>?> GetToplist();
  public Task<CoinData?> GetCertainAssetAsync(string assetId);
}
