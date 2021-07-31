using ApplicationServices.Business;
using ApplicationServices.Business.IAppServices;
using FalconITScrapperWebAPI.ActionFilters;
using LoggerService;
using LoggerService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalconITScrapperWebAPI.ServiceInjection
{
    public static class ServiceInjection
    {
        public static void AddPersistanceInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {

            #region repos
            service.AddTransient<ISearchNumberPlate, SearchNumberPlate>();

            #endregion
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
