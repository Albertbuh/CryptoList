namespace CryptographyAssets;
public interface ICryptAssetsService
{
  public Task<IEnumerable<ICurrencyAssetData>?> GetToplist();
  public Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId);
}
