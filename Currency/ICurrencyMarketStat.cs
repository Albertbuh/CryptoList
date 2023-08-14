namespace CurrencyBase;

public interface ICurrencyMarketStat
{
  public decimal MarketCap { get; }
  public double Change { get; set; }
}
