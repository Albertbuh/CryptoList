namespace CurrencyBase;

public interface ICurrencyEssense
{
  public string Id { get; }
  public string Name { get; set; }
  public decimal Price { get; }
}
