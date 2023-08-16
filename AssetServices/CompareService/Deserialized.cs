namespace CryptographyAssets.CompareService;

public record class DeserializedToplist(
    CompareToplistItem[]? Data,
    string Response,
    string Message
    );

public record class CompareToplistItem(ToplistCoinInfo? CoinInfo, ToplistRaw? Raw);

public record class ToplistCoinInfo(string Name, string FullName);
public record class ToplistRaw(ToplistUsd? Usd);

public record class ToplistUsd(
      decimal Price,
      double ChangePct24Hour,
      decimal Volume24HourTo,
      decimal TotalVolume24hTo,
      decimal TotalTopTierVolume24hTo,
      decimal MktCap
      );

record class DeserializedIcon(IconData Data);
record class IconData(string Logo_Url); 

