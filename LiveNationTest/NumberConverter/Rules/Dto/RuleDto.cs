using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Rules.Dto
{
    public class RuleDto
    {
        public RuleDto(string ruleOperator, int operand, int expectedResult, string replacementString)
        {
            RuleOperator = ruleOperator;
            Operand = operand;
            ExpectedResult = expectedResult;
            ReplacementString = replacementString;
        }

        public string RuleOperator { get; init; }
        public int Operand { get; init; }
        public int ExpectedResult { get; init; }
        public string ReplacementString { get; init; }
    }
}
