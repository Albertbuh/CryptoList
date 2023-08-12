namespace CryptographyAssets;
public interface ICryptAssetsService
{
  public Task<IEnumerable<ICurrencyAssetData>?> GetAllAssetsAsync();
  public Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId);
}
