namespace Geo;

public interface IGeoService
{
  public Task<IResult> Init(HttpRequest request);
  public string Currency { get; set; }
}
