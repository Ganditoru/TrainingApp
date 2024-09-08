namespace TrainingApp.Domain_Layer.Interfaces
{
    public interface IWeatherForecastRepository
    {

        IEnumerable<WeatherForecast> GetAll();
    }
}
