namespace CryptoCurrencyService.Compare;

public class CompareToplistCurrencyData : ToplistCurrencyData
{
  private CompareToplistItem data;

  public CompareToplistCurrencyData(CompareToplistItem data)
  {
    this.data = data;
    SetCurrency();
  }

  private bool CoinInfoIsNull => data.CoinInfo == null;
  private bool RawIsNull => data.Raw == null;
  private bool UsdIsNull => RawIsNull || data.Raw!.Usd == null;

  public void SetIconUrl(string url)
  {
    SetIcon(url);
  }

  private void SetCurrency()
  {
    SetEssense();
    SetVolume();
    SetMarketStat();
  }

  private void SetMarketStat()
  {
    decimal marketCap = 0;
    double change = 0;
    if (!UsdIsNull)
    {
      marketCap = data.Raw!.Usd!.MktCap;
      change = data.Raw!.Usd!.ChangePct24Hour;
    }
    SetMarketStat(marketCap, change);
  }

  private void SetVolume()
  {
    decimal direct = -1;
    decimal total = -1;
    decimal topTier = -1;
    if (!UsdIsNull)
    {
      direct = data.Raw!.Usd!.Volume24HourTo;
      total = data.Raw.Usd.TotalVolume24hTo;
      topTier = data.Raw.Usd.TotalTopTierVolume24hTo;
    }
    SetVolume(direct, total, topTier);
  }

  private void SetEssense()
  {
    string id = "null";
    string name = "undefiend";
    decimal price = -1;
    if (!CoinInfoIsNull)
    {
      id = data!.CoinInfo!.Name;
      name = data.CoinInfo.FullName;
    }

    if (!UsdIsNull)
      price = data.Raw!.Usd!.Price;
    SetEssense(id, name, price);
  }
}
