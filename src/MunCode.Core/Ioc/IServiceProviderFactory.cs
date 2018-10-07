namespace MunCode.Core.Ioc
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public interface IServiceProviderFactory
    {
        IServiceProvider Create(IServiceCollection services);
    }
}