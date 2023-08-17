namespace Geo;

public interface IGeoService
{
  public Task<IResult> Init(HttpRequest request);
  public string Currency { get; set; }
}

public class GeoapifyService : IGeoService
{
  public GeoData? Data;
  private string _currency = "";
  public string Currency { get => _currency; set => _currency = value;}
  private ILogger logger;

  public GeoapifyService(ILoggerFactory factory)
  {
    logger = factory.CreateLogger("GeoService");
  }

  public async Task<IResult> Init(HttpRequest request)
  {
    Data = await request.ReadFromJsonAsync<GeoData>();
    if (Data != null)
    {
      logger.LogInformation($"New user from {Data.Country.Name} {Data.City.Name}");
      _currency = Data.Country.Currency;
      return Results.Ok("ok");
    }
    else
      return Results.BadRequest("Cannot read geodata from service");
  }
}

public record class GeoData(
  City City,
  Continent Continent,
  Country Country,
  Location Location,
  string Ip
);

public record class City(string Name);

public record class Continent(string Code, long Geoname_Id, string Name);

public record class Country(
  long Geoname_Id,
  string Name,
  string Name_Native,
  string PhoneCode,
  string Capital,
  string Currency,
  string Flag
);

public record class Location(double Latitude, double Longtitude);
