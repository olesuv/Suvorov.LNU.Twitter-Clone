using Microsoft.AspNetCore.Mvc.RazorPages;
using Suvorov.LNU.TwitterClone.Core.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Weather;

namespace Suvorov.LNU.TwitterClone.Web.Pages
{
    public class WeatherForecastModel : PageModel
{
    public IList<WeatherForecast> Forecasts { get; set; }

    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastModel(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    public void OnGet()
    {
        Forecasts = _weatherForecastService.GetRandomForecast().ToList();
    }
}
}