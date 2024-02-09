using NumberConverter.Rules.Dto;
using NumberConverter.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Rules
{
    public class RuleService
    {
        private readonly IRuleStore _ruleStore;

        public RuleService(IRuleStore ruleStore)
        {
            _ruleStore = ruleStore;
        }

        public RuleValidationResult AddRule(RuleDto rule)
        {
            RuleValidationResult result = ValidateRule(rule);

            if (result.Success && result.Rule != null)
            {
                _ruleStore.StoreRule(result.Rule);
            }

            return result;
        }

        private RuleValidationResult ValidateRule(RuleDto rule)
        {
            if (!RuleOperator.TryParse(rule.RuleOperator, true, out RuleOperator parsedOperator))
            {
                return new RuleValidationResult(false, "Invalid Operator: valid operators are: DivisibleBy, Multiplication, Addition, Subtraction", null);
            }

            if (parsedOperator == RuleOperator.DivisibleBy && rule.Operand == 0)
            {
                return new RuleValidationResult(false, "Invalid Operand: cannot divide by 0", null);
            }

            if (parsedOperator == RuleOperator.Multiplication && rule.Operand == 0)
            {
                return new RuleValidationResult(false, "Invalid Operand: cannot multiply by 0", null);
            }

            if (String.IsNullOrEmpty(rule.ReplacementString))
            {
                return new RuleValidationResult(false, "Invalid ReplacementString: ReplacementString cannot be empty", null);
            }

            return new RuleValidationResult(true, "", new Rule() { 
                RuleOperator = parsedOperator, 
                Operand = rule.Operand, 
                ExpectedResult = rule.ExpectedResult,
                ResultString = rule.ReplacementString 
            });
        }
    }
}
