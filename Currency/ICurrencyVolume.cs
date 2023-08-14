namespace CurrencyBase;

public interface ICurrencyVolume
{
  public decimal Direct { get; }
  public decimal Total { get; }
  public decimal TopTier { get; }
}
