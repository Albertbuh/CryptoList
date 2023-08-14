namespace CryptographyAssets.CompareService;

public class DeserializedVolumeArray
{
  public VolumeData[]? Data { get; set; }
  public string Response { get; set; } = "Error";

  public class VolumeData
  {
    public string Exchange { get; set; } = "";
    public string FromSymbol { get; set; } = "";
    public decimal Price { get; set; }
  }
}

public record class DeserializedVolumeError(string Response, string Message);

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
