using TrainingApp.Domain_Layer.Interfaces;

namespace TrainingApp.Infrastructure_Layer.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public IEnumerable<WeatherForecast> GetAll()
        {
            return Enumerable.Range(1, 9).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
