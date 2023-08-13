namespace CryptographyAssets.CompareService;

public class CurrencyAssetData : ICurrencyAssetData
{
  private DeserializedVolumeArray obj;

  public CurrencyAssetData(DeserializedVolumeArray array)
  {
    this.obj = array;
  }

  public string AssetId
  {
    get => obj.Data![0].FromSymbol;
  }
  public string Name
  {
    get => String.Join(",", obj.Data!.Select(x => x.Exchange));
    set => obj.Data![0].Exchange = value;
  }
  public decimal PriceUsd
  {
    get => obj.Data!.Average(x => x.Price);
    set => obj.Data![0].Price = value;
  }
}


