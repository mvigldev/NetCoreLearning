using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;;

namespace NetCoreLearning.SchoolLessons.API.Tests
{
    public static class Helper
    {
        public static ILogger<T> GetLogger<T>() where T : class
        {
            var serviceProvider = new ServiceCollection()
              .AddLogging()
              .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            return factory.CreateLogger<T>();
        }

        public static IMemoryCache GetMemoryCache()
        {
            var serviceProvider = new ServiceCollection()
              .AddMemoryCache()
              .BuildServiceProvider();

            return serviceProvider.GetService<IMemoryCache>();
        }
    }
}
