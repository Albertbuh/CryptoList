namespace CryptographyAssets;

using CurrencyBase.Toplist;

public interface ICryptAssetsService
{
  public Task<IEnumerable<ToplistCurrencyData>?> GetToplist();
  public Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId);
}
