using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace together_aspcore.Test
{
    public class DependencyResolverHelpercs
    {
        private readonly IWebHost _webHost;

        /// <inheritdoc />
        public DependencyResolverHelpercs(IWebHost webHost) => _webHost = webHost;

        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}