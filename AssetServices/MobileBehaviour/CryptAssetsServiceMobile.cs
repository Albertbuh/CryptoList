namespace CryptographyAssets.Mobile;

using CurrencyBase.Mobile;
using CurrencyBase.Toplist;

public class CryptAssetsServiceMobile : ICryptAssetsService
{
  private ICryptAssetsService service;

  public CryptAssetsServiceMobile(ICryptAssetsService service)
  {
    this.service = service;
  }

  public async Task<IEnumerable<ToplistCurrencyData>?> GetToplist()
  {
    return await service.GetToplist();
  }

  public async Task<IEnumerable<MobileToplistCurrencyData>?> GetMobileToplist()
  {
    var currencyData = await GetToplist();
    return currencyData?.Select(
      data =>
        new MobileToplistCurrencyData(data.Id, data.Name, data.Price, data.MarketCap, data.Change)
    );
  }

  public Task<ICurrencyAssetData?> GetCertainAssetAsync(string assetId)
  {
    throw new NotImplementedException();
  }
}
