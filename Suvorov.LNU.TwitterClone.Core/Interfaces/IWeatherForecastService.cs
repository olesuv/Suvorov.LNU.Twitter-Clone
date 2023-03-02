using Suvorov.LNU.TwitterClone.Models.Weather;

namespace Suvorov.LNU.TwitterClone.Core.Interfaces
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetRandomForecast();
    }
}
