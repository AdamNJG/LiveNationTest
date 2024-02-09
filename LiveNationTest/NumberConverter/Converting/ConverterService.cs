using NumberConverter.Converting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Converting
{
    public class ConverterService
    {
        IConverterRuleStore _ruleStore;

        public ConverterService(IConverterRuleStore ruleStore)
        {
            _ruleStore = ruleStore;
        }

        public ConverterResult Parse(int start, int end)
        {
            Converter parser = new Converter(_ruleStore.GetRules()
                .Select(r => new ConverterRule(r.RuleOperator, r.Operand, r.ExpectedResult, r.ResultString)).ToList());
            return parser.Parse(start, end);
        }
    }
}
