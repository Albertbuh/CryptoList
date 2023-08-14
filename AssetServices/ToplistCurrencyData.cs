namespace CurrencyBase.Toplist;

public abstract class ToplistCurrencyData : ICurrencyEssense, ICurrencyVolume, ICurrencyMarketStat, ICurrencyIcon
{
  public string Id { get; set; } = "";
  public string Name { get; set; } = "";
  public decimal Price { get; set; }

  //Volume
  public decimal Direct { get; set; }
  public decimal Total { get; set; }
  public decimal TopTier { get; set; }

  public decimal MarketCap { get; set; }
  public double Change { get; set; }

  public string IconUrl {get; set;} = "";

  protected virtual void SetIcon(string url)
  {
    IconUrl = url;
  }

  protected virtual void SetEssense(string id, string name, decimal price)
  {
    Id = id;
    Name = name;
    Price = price;
  }

  protected virtual void SetVolume(decimal direct, decimal total, decimal topTier)
  {
    Direct = direct;
    Total = total;
    TopTier = topTier;
  }

  protected virtual void SetMarketStat(decimal marketCap, double change)
  {
    MarketCap = marketCap;
    Change = change;
  }
}
