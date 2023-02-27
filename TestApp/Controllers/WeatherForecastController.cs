using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data; 
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Unbearable"
        };
        private readonly AppSettings _appSettings;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IOptions<AppSettings> options, ILogger<WeatherForecastController> logger)
        {
            _appSettings = options.Value;
            _logger = logger;
        }

        [HttpGet("WeatherGetTest")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        // this commit should trigger the release
        [HttpGet("DbTest")]
        public async Task<IActionResult> DbTest()
        {
            using (IDbConnection con = await ConnectAsync())
            {
                var sql = @"SELECT 
                                ChequeStatusId,
                                ChequeStatusName,
                                DateAdded,
                                DateEdited,
                                AddedBy,
                                EditedBy,
                                IsActive
                            FROM  chequestatus;";
                var result = await con.QueryAsync<ChequeStatus>(sql, null, commandType: CommandType.Text);
                return Ok(result);
            }
        }


        private async Task<IDbConnection> ConnectAsync()
        {
            var connectionString = _appSettings.ConnectionStrings?.DbCon;
            var dbConn = new MySqlConnection(connectionString);
            await dbConn.OpenAsync();
            return dbConn;
        }
    }
}