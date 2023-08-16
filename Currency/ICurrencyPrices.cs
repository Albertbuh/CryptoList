namespace CurrencyBase;

public interface ICurrencyPrices
{
  public decimal UsdPrice { get; set; }
  public decimal EurPrice { get; set; }
  public decimal UserCountryPrice { get; set; }
}
