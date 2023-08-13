namespace CryptographyAssets.CompareService;
public class DeserializedVolumeArray
{
  public VolumeData[]? Data {get; set;}
  public string Response {get; set;} = "Error";

  public class VolumeData 
  {
    public string Exchange {get; set;} = "";
    public string FromSymbol {get; set;} = "";
    public decimal Price {get; set;}
  }
}

public class DeserializedVolumeError
{
   public string Response {get; set;} = "Error";
   public string Message {get; set;} = "";
}

public class VolumeCurrencyAssetData : ICurrencyAssetData
{
  private DeserializedVolumeArray obj;
  public VolumeCurrencyAssetData(DeserializedVolumeArray array)
  {
    this.obj = array;
  }

  public string AssetId {get => obj.Data![0].FromSymbol;}
  public string Name {get => String.Join(",", obj.Data!.Select(x => x.Exchange)); set => obj.Data![0].Exchange = value;}
  public decimal PriceUsd {get => obj.Data!.Average(x => x.Price); set => obj.Data![0].Price = value;}
}


public class DeserializedToplist
{
   public ToplistData[]? Data {get; set;}
   public string Response {get; set;} = "Error";
   public string Message {get; set;} = "";
}


public class ToplistData 
{
 public ToplistCoinInfo? CoinInfo {get; set;} 
 public ToplistRaw? Raw {get; set;}

 public class ToplistCoinInfo
 {
   public string Name {get; set;} = "";
   public string FullName {get; set;} = "";
 }

 public class ToplistRaw
 {
   public ToplistUsd? Usd {get; set;}

   public class ToplistUsd 
   {
     public decimal Price {get; set;}
   }
 }
}

public class ListCurrencyAssetData : ICurrencyAssetData
{
  private ToplistData data;
  public ListCurrencyAssetData(ToplistData data)
  {
    this.data = data;
  }

  public string AssetId {get => data.CoinInfo!.Name;}
  public string Name {get => data.CoinInfo!.FullName; set => data.CoinInfo!.FullName = value;}
  public decimal PriceUsd {
    get  {
      if(data.Raw != null && data.Raw.Usd != null)
        return data.Raw.Usd.Price;
      else
        return -1;
    } 
  set {
      if(data.Raw != null && data.Raw.Usd != null)
        data.Raw.Usd.Price = value;
    }
  }
}
