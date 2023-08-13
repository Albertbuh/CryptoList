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

public class DeserializedVolumeError
{
  public string Response { get; set; } = "Error";
  public string Message { get; set; } = "";
}

public class DeserializedToplist
{
  public CompareToplistItem[]? Data { get; set; }
  public string Response { get; set; } = "Error";
  public string Message { get; set; } = "";
}

public class CompareToplistItem 
{
  public ToplistCoinInfo? CoinInfo { get; set; }
  public ToplistRaw? Raw { get; set; }

  public class ToplistCoinInfo
  {
    public string Name { get; set; } = "";
    public string FullName { get; set; } = "";
  }

  public class ToplistRaw
  {
    public ToplistUsd? Usd { get; set; }

    public class ToplistUsd
    {
      public decimal Price { get; set; }

      //Change field
      public double ChangePct24Hour { get; set; }

      //DirectVolume field
      public decimal Volume24HourTo { get; set; }

      //Total Volume
      public decimal TotalVolume24hTo { get; set; }

      //TopTier Volume
      public decimal TotalTopTierVolume24hTo { get; set; }

      //Market Cap
      public decimal MktCap { get; set; }
    }
  }
}
