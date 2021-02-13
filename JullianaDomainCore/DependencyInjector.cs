using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using JullianaDomainCore.Entity;

namespace JullianaDomainCore
{
    public static class DependencyInjector
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddDbContext<JullianaDbContext>();
            services.AddDbContext<IdentityContext>();

            services.AddTransient<IDbContext, JullianaDbContext>();
        }
    }
}