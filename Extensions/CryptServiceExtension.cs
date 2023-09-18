using CryptoCurrencyService;
namespace Extensions.CryptService;

public static class AssetsServiceExtension
{
  public static void AddCryptService<T, TProxy>(this IServiceCollection serviceCollection)
    where T : class, ICryptAssetsService
    where TProxy : class, ICryptAssetsService
  {
    serviceCollection.AddSingleton<ICryptAssetsService, T>();
    serviceCollection.AddSingleton<TProxy>();
  }

  public static void AddCryptService<T>(this IServiceCollection serviceCollection)
    where T : class, ICryptAssetsService
  {
    serviceCollection.AddSingleton<ICryptAssetsService, T>();
  }

  public static T? GetCryptService<T>(this IServiceProvider services)
    where T : class, ICryptAssetsService
  {
    return services.GetService<T>();
  }
}
