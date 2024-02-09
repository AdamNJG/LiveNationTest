using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberConverter.Converting;
using NumberConverter.Rules.Dto;
using NumberConverter.Rules;
using Swashbuckle.AspNetCore.Annotations;

namespace LiveNationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly RuleService _ruleService;
        private readonly ILogger<NumberConverterController> _logger;

        public RulesController(RuleService ruleService, ILogger<NumberConverterController> logger)
        {
            _ruleService = ruleService;
            _logger = logger;
        }

        [HttpPost(Name = "AddRule")]
        public IActionResult AddRule([FromBody] RuleDto rule)
        {
            try
            {
                RuleValidationResult result = _ruleService.AddRule(rule);

                if (result.Success)
                {
                    return Ok();
                }
                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
