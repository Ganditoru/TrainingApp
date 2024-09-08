using TrainingApp.Domain_Layer.Interfaces;

namespace TrainingApp.Application_Layer.Services
{
    public class WeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public IEnumerable<WeatherForecast> GetAllForecasts()
        {
            return _weatherForecastRepository.GetAll();
        }
    }
}
