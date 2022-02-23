using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Person.Business.Contract.Services;
using VY.Person.Business.Impl.MappingProfiles;
using VY.Person.Business.Impl.Services;
using VY.Person.Data.Impl.Extensions;

namespace VY.Person.Business.Impl.Extensions
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(PersonProfile));
            services.AddTransient<IPersonService, PersonService>();
            services.AddDataServices(configuration);
            return services;
        }
    }
}
