using System.Linq;
using Grocery.Api.ConfigExtensions;
using Grocery.Data;
using Grocery.Data.Interfaces;
using Grocery.Data.Repositories;
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
      
      services.AddCosmosDbConfiguration(Configuration);
      services.AddControllers();
      

      //services.Configure<CosmosDbOptions>(Configuration.GetSection("CosmosDb"));
      //services.AddScoped<ICosmosDbClient, CosmosDbClient>();
      //services.AddScoped<ICategoryRepository, CategoryRepository>();
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
