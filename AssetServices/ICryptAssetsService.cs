namespace CryptographyAssets;

using CurrencyBase.Toplist;
using CurrencyBase.Coin;

public interface ICryptAssetsService
{
  public Task<IEnumerable<ToplistCurrencyData>?> GetToplist();
  public Task<CoinData?> GetCertainAssetAsync(string assetId);
}
