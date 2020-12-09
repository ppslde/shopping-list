using Grocery.Data;
using Grocery.Data.Interfaces;
using Grocery.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Api.ConfigExtensions {
  public static class CosmosDbConfigExtensions {


    public static IServiceCollection AddCosmosDbConfiguration(this IServiceCollection services, IConfiguration appConfig) {

      services.Configure<CosmosDbOptions>(appConfig.GetSection("CosmosDb"));
      services.AddScoped<ICosmosDbClient, CosmosDbClient>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      return services;
    }
  }
}
