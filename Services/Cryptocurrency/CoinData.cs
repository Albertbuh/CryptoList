using CurrencyBase;

namespace CryptoCurrencyService;

public abstract class CoinData : ICurrencyPrices, ICurrencyEssense
{
  public decimal UsdPrice { get; set; }
  public decimal EurPrice { get; set; }
  public decimal UserCountryPrice { get; set; }

  public string Id { get; set; } = "";
  public string Name { get; set; } = "";
  public decimal Price { get; set; }

  public string Description { get; set; } = "";
  public string AssetType { get; set; } = "";
  public string LogoUrl { get; set; } = "";
  public string WebsiteUrl { get; set; } = "";

  protected virtual void SetPrices(decimal usd = 0, decimal eur = 0, decimal user = 0)
  {
    UsdPrice = usd;
    EurPrice = eur;
    UserCountryPrice = user;
    Price = UsdPrice;
  }

  protected virtual void SetEssense(
    string id,
    string name,
    decimal? price = null,
    string? type = null
  )
  {
    Id = id;
    Name = name;
    if (price != null)
      Price = price ?? 0;
    if (type != null && !String.IsNullOrEmpty(type))
      AssetType = type;
  }

  protected virtual void SetCoinInfo(string desc, string type)
  {
    Description = desc;
    AssetType = type;
  }

  protected virtual void SetUrls(string logo, string website)
  {
    LogoUrl = logo;
    WebsiteUrl = website;
  }
}
