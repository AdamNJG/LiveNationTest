using NumberConverter.Converting.Dto;
using NumberConverter.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverterTest.Parsing.helpers
{
    public static class ConverterServiceHelper
    {
        public static List<RuleDto> DefaultRules()
        {
            return new List<RuleDto>()
            {
                new RuleDto {
                    RuleOperator = RuleOperator.DivisibleBy,
                    Operand = 3,
                    ExpectedResult = 0,
                    ResultString = "Live" 
                },
                new RuleDto {
                    RuleOperator = RuleOperator.DivisibleBy,
                    Operand = 5,
                    ExpectedResult = 0,
                    ResultString = "Nation"
                }
            };
        }
    }
}
