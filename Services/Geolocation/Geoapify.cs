namespace Geo;

public class GeoapifyService : IGeoService
{
  public GeoData? Data;
  public string Currency {get; set;} = "";
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
      logger.LogInformation($"Connected new user from {Data.Country.Name} {Data.City.Name}");
      Currency = Data.Country.Currency;
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
