using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace NumberConverter.Converting
{
    internal class Converter
    {
        private readonly List<ConverterRule> _rules;

        public Converter(List<ConverterRule> rules)
        {
            _rules = rules;
        }

        public ConverterResult Parse(int start, int end)
        {
            // Posibility for DDD style Number class that applies rules on creation, could be over-engineering at the moment? needs more thought

            if (InputsNegative(start, end))
            {
                return new ConverterResult("Integers may not be negative", new Dictionary<string, int> { { "Error", 1 } });
            }

            Dictionary<string, int> counts = new Dictionary<string, int>();
            List<int> range = Enumerable.Range(start, end).ToList();
            StringBuilder builder = new StringBuilder();

            range.ForEach(number =>
            {
                List<ConverterRule> applicableRules = _rules.Where(r => r.Check(number))
                    .ToList();

                if (applicableRules.Count == 0)
                {
                    builder.Append(number).Append(" ");
                    counts = AddToCount(counts, "Integer");
                    return;
                }

                StringBuilder resultBuilder = new ();

                applicableRules
                    .ForEach(r =>
                    {
                        resultBuilder.Append(r.GenerateString());
                    });

                counts = AddToCount(counts, resultBuilder.ToString());
                builder.Append(resultBuilder.ToString()).Append(" ");
            });

            if (builder.Length > 0)
            {
                builder.Length--;
            }

            return new ConverterResult(builder.ToString(), counts);
        }

        private Dictionary<string, int> AddToCount(Dictionary<string, int> count, string ruleString)
        {
            if (count.TryGetValue(ruleString, out int currentValue))
            {
                count[ruleString] = ++currentValue;
            }
            else
            {
                count[ruleString] = 1;
            }

            return count;
        }

        private bool InputsNegative(int start, int finish)
        {
            return start < 0 || finish < 0;
        }
    }

}
