using System.Data;
using burgershack.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace burgershack
{
  public class Startup
  {
    private readonly string _connectionString = "";
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      _connectionString = configuration.GetSection("DB").GetValue<string>("mySQLConnectionString");
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // just like let bp = require(body-parser)
      // app.use(bp.json())
      // app.use(bp.urlencoded({extended: true}))
      // app hasnt started yet - all about pulling 3rd party libraries and configuring them
      // combined wth app.UseMvc() down below - sets up the project to work to a specific pattern; 
      // dont name things wrong/put in wrong spot, it won't be registered 
      services.AddTransient<IDbConnection>(x => CreateDBContext());

      services.AddTransient<BurgersRepository>(); // if you forget this it wont work (:
      services.AddTransient<SmoothiesRepository>();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    private IDbConnection CreateDBContext()
    {
      var connection = new MySqlConnection(_connectionString); // this is where you paste in your connection string and then this all magically works yayyiu 
      connection.Open();
      return connection;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      // configures the 3rd party libraries - (app.use(bp))
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}
