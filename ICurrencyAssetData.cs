namespace CryptographyAssets;

public interface ICurrencyAssetData
{
  public string AssetId {get;}
  public string Name {get; set;}
  public decimal PriceUsd {get; set;}
}


