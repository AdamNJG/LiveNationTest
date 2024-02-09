using NumberConverter;
using NumberConverter.Converting.Dto;
using NumberConverter.Converting.Interfaces;
using NumberConverter.Rules;
using NumberConverter.Rules.Interfaces;

namespace RuleRepository
{
    public class RuleStore : IConverterRuleStore, IRuleStore
    {
        private readonly RuleContext _context;
        public RuleStore(RuleContext context)
        {
            _context = context;
        }

        public List<RuleDto> GetRules()
        {
            return _context.Rules
                .Select(r => new RuleDto()
                {
                    RuleOperator = r.RuleOperator,
                    Operand = r.Operand,
                    ExpectedResult = r.ExpectedResult,
                    ResultString = r.ResultString,
                }).ToList();
        }

        public void StoreRule(Rule rule)
        {
            _context.Rules.Add(rule);
            _context.SaveChanges();
        }
    }
}
