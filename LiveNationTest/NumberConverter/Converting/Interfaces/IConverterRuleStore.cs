
using NumberConverter.Converting.Dto;

namespace NumberConverter.Converting.Interfaces
{
    public interface IConverterRuleStore
    {
        List<RuleDto> GetRules();
    }
}
