namespace MunCode.Core.AspNetCore.Host
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;

    using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            services.AddMvc();
            services.AddSingleton<IHostedService, HostedService>();
            var serviceProviderInitializer = new ServiceProviderInitializer();
            return serviceProviderInitializer.Initialize(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Guard.NotNull(env, nameof(env));
            Guard.NotNull(app, nameof(app));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
