using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application_Layer.Services;

namespace TrainingApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]//{version:apiVersion}
    [ApiVersion("1.0")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Weather data fetched at {Time}", DateTime.Now);

            return _weatherForecastService.GetAllForecasts();
        }
    }
}
