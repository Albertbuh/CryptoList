namespace CryptographyAssets.CompareService;

using CurrencyBase.Coin;
using System.Text.RegularExpressions;

public class CompareCoinData : CoinData
{
  private DeserializedCoinData data;
  public CompareCoinData(DeserializedCoinData data)
  {
    this.data = data;  
    SetEssense(data.Data.Symbol, data.Data.Name);
    SetCoinInfo(data.Data.Asset_Description, data.Data.Asset_Type);
    SetUrls(data.Data.Logo_Url, data.Data.Website_Url);
  }
  
  public void SetPrices(string pricesString)
  {
    Regex regex = new Regex(@"(\d+)\.(\d*)");
    MatchCollection matches = regex.Matches(pricesString);
    int count = matches.Count;
    decimal[] prices = new decimal[count];
    for (int i = 0; i < count; i++)
    {
      Decimal.TryParse(matches[i].Value, out prices[i]);
    }
    switch (count)
    {
      case 1:
        SetPrices(prices[0]);
        break;
      case 2:
        SetPrices(prices[0], prices[1]);
        break;
      case 3:
        SetPrices(prices[0], prices[1], prices[2]);
        break;
    }
  }

}
