// namespace CryptographyAssets;
//
// public class CoinCurrencyAssetData : IAssetVolume
// {
//   private CoinJsonData json;
//
//   public CoinCurrencyAssetData(CoinJsonData data)
//   {
//     json = data;
//   }
//
//   public string AssetId
//   {
//     get => json.Asset_Id;
//   }
//   public string Name
//   {
//     get => json.Name;
//     set => json.Name = value;
//   }
//   public decimal PriceUsd
//   {
//     get => json.Price_Usd;
//     set => json.Price_Usd = value;
//   }
//
//   public decimal DirectVolume { get; set; }
//   public decimal TotalVolume { get; set; }
//   public decimal TopTierVolume { get; set; }
//   public decimal MarketCap { get; set; }
//   public decimal Change { get; set; }
// }
//
// public class CoinJsonData
// {
//   public string Asset_Id { get; set; } = "";
//   public string Name { get; set; } = "";
//   public decimal Price_Usd { get; set; }
// }
