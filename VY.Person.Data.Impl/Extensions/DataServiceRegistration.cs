using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Person.Data.Contract.Repositories;

namespace VY.Person.Data.Impl.Extensions
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IFilePersonRepository>(c => new FilePersonRepository(configuration["Filepath"]));
            return service;
        }
    }
}
