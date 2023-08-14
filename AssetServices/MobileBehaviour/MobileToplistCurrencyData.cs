namespace CurrencyBase.Mobile;

public class MobileToplistCurrencyData : ICurrencyEssense, ICurrencyMarketStat
{
  public string Id { get; set; }
  public string Name { get; set; }
  public decimal Price { get; set; }

  public decimal MarketCap { get; set; }
  public double Change { get; set; }

  public MobileToplistCurrencyData()
    : this("", "", 0, 0, 0) { }

  public MobileToplistCurrencyData(
    string id,
    string name,
    decimal price,
    decimal market,
    double change
  )
  {
    Id = id;
    Name = name;
    Price = price;
    MarketCap = market;
    Change = change;
  }
}
