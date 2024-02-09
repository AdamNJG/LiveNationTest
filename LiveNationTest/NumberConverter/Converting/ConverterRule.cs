
using NumberConverter.Rules;

namespace NumberConverter.Converting
{
    public class ConverterRule
    {
        private Predicate<int> _predicate;
        private string _resultString;

        public ConverterRule(RuleOperator ruleOperator, int operand, int expectedResult, string resultString)
        {
            _predicate = GeneratePredicate(ruleOperator, operand, expectedResult);
            _resultString = resultString;
        }

        public bool Check(int input)
        {
            return _predicate(input);
        }

        public string GenerateString()
        {
            return _resultString;
        }

        private Predicate<int> GeneratePredicate(RuleOperator operation, int operand, int expectedOutput) =>
            operation switch
            {
                RuleOperator.DivisibleBy => (x) => x % operand == expectedOutput,
                RuleOperator.Multiplication => (x) => x * operand == expectedOutput,
                RuleOperator.Addition => (x) => x + operand == expectedOutput,
                RuleOperator.Subtraction => (x) => x - operand == expectedOutput,
                _ => throw new ArgumentOutOfRangeException($"Not expected direction value: {operation}")
            };
    }
}
