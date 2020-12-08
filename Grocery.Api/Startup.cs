using System.Linq;
using Grocery.Api.ConfigExtensions;
using Grocery.Data;
using Grocery.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Grocery.Api {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();

      var connectionStringOptions = Configuration.GetSection("ConnectionString").Get<ConnectionStringOptions>();
      var cosmosDbOptions = Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
      var (serviceEndpoint, authKey) = connectionStringOptions;
      var (databaseName, collectionData) = cosmosDbOptions;
      var collectionNames = collectionData.Select(c => c.Name).ToList();

      services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);

      services.AddScoped<GroceryItemRepository, GroceryItemRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
