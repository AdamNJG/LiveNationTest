using NumberConverter.Rules;

namespace NumberConverter.Converting.Dto
{
    public class RuleDto
    {
        public RuleOperator RuleOperator { get; set; }
        public int Operand { get; set; }
        public int ExpectedResult { get; set; }
        public string ResultString { get; set; }
    }
}
