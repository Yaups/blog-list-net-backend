using Microsoft.AspNetCore.Mvc;

namespace blog_list_net_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        public BlogsController()
        {
        }

        [HttpGet(Name = "blogs")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
