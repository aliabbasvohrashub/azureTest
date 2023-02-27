using Microsoft.AspNetCore; 
using Serilog;
using TestApp;

string? AppName = typeof(Startup).Namespace;

var configuration = GetConfiguration();

Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.ForContext<Program>();//.Error("Test Number {Parm}", "1");
    Log.Information("Configuring web host ({ApplicationContext})...", AppName);
    var host = BuildWebHost(configuration, args);

    Log.Information("Starting web host ({ApplicationContext})...", AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
    WebHost.CreateDefaultBuilder(args)
    .CaptureStartupErrors(true)
    .ConfigureAppConfiguration(app => app.AddConfiguration(configuration))
    .UseStartup<Startup>()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseSerilog()
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

//var Configuration = GetConfiguration();
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddOptions();
//builder.Services.Configure<AppSettings>(Configuration);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



IConfiguration GetConfiguration()
{
    string hotingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{hotingEnvironment}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
    var config = builder.Build();

    return config;
}


Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    return new LoggerConfiguration()
         .MinimumLevel.Verbose()
         .Enrich.WithProperty("ApplicationContext", AppName)
         .Enrich.FromLogContext()
         .WriteTo.Console()
         .WriteTo.File("log/azure_testApp-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}


namespace TestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //LogManager.LoadConfiguration(string.Format("{0}/nlog.config", Directory.GetCurrentDirectory()));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Read appsettings.json
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddOptions();
            services.Configure<AppSettings>(Configuration); 

            #endregion Read appsettings.json  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
             

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseCors("_myAllowSpecificOrigins");
            app.UseHttpsRedirection();
            //app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test App Services"));
        }
    }
}
