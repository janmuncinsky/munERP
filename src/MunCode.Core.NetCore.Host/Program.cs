namespace MunCode.Core.NetCore.Host
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    using MunCode.Core.Ioc;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
                .UseServiceProviderFactory(new AppBuilderServiceProviderFactory())
                .RunConsoleAsync();
        }
    }
}
