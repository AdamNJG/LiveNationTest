using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Rules
{
    public class Rule
    {
        public int Id { get; set; }
        public RuleOperator RuleOperator { get; set; }
        public int Operand { get; set; }
        public int ExpectedResult { get; set; }
        public string ResultString { get; set; }
    }
}
