using NumberConverter.Converting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LiveNationTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumberConverterController : ControllerBase
    {
        private readonly ConverterService _numberService;
        private readonly ILogger<NumberConverterController> _logger;
        private readonly IMemoryCache _cache;

        public NumberConverterController(ConverterService numberService, ILogger<NumberConverterController> logger, IMemoryCache cache)
        {
            _numberService = numberService;
            _logger = logger;
            _cache = cache;
        }

        [HttpPost(Name = "Convert")]
        public IActionResult Convert(int first, int last)
        {
            try
            {
                string cacheKey = $"ConverterResult_{first}_{last}";

                if (_cache.TryGetValue(cacheKey, out ConverterResult cachedResult))
                {
                    return Ok(cachedResult);
                }

                ConverterResult result = _numberService.Parse(first, last);

                if (result.summary.ContainsKey("Error"))
                {
                    return BadRequest(result.result);
                }

                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}