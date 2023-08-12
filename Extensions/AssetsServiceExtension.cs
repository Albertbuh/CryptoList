namespace CryptographyAssets;
public static class AssetsServiceExtension
{
  public static void AddAssetsService<T,TProxy>(this IServiceCollection serviceCollection)
    where T : class, ICryptAssetsService
    where TProxy : class, ICryptAssetsService
  {
    serviceCollection.AddSingleton<ICryptAssetsService, T>();
    serviceCollection.AddSingleton<TProxy>();
  }

  public static void AddAssetsService<T>(this IServiceCollection serviceCollection)
    where T : class, ICryptAssetsService
  {
    serviceCollection.AddSingleton<ICryptAssetsService, T>();
  }

  public static T? GetAssetsService<T>(this IServiceProvider services)
    where T: class, ICryptAssetsService
  {
    return services.GetService<T>();
  }
}

