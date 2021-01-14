using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Desorganizze.Infra.CQRS
{
    public class DependencyResolver
    {
        private readonly IServiceProvider serviceProvider;

        public DependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TDependencyType Resolve<TDependencyType>()
        {
            return serviceProvider.GetRequiredService<TDependencyType>();
        }

        public IEnumerable<TDependencyType> ResolveAll<TDependencyType>()
        {
            return serviceProvider.GetServices<TDependencyType>();
        }
    }
}
