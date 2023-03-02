using Microsoft.AspNetCore.Mvc;
using Suvorov.LNU.TwitterClone.Core.Interfaces;
using Suvorov.LNU.TwitterClone.Models.Weather;

namespace Suvorov.LNU.TwitterClone.API.Controllers
{
    [ApiController]
[Route("/api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _forecastService;

    public WeatherForecastController(IWeatherForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpGet]
    [Route("Get")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _forecastService.GetRandomForecast();
    }
}
}