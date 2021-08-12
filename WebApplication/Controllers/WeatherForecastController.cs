using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IMemoryCache cache, ILogger<WeatherForecastController> logger)
        {

            //var result = cache.GetOrCreate("key-1", entry => _service.GetObject());
            //return result;
                
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpPost("customer/{customerId}/invocice/{invoiceId}/task/{taskId}")]
        public IActionResult Post([FromHeader] Dummy obj)
        {
            return null;
        }
        
    }

    public class Dummy
    {
        [FromBody]
        public string Ctx { get; set; }
        public int CustomerId { get; set; }
    }
}