using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            ConversionContext context = new ();
            foreach (int number in Enumerable.Range(start, end).ToList())
            {
                context = ProcessNumber(context, number);
            }

            return new ConverterResult(context.Builder.ToString().TrimEnd(), context.Count);
        }

        private ConversionContext ProcessNumber(ConversionContext context, int number)
        {
            List<ConverterRule> applicableRules = _rules.Where(r => r.Check(number))
                .ToList();

            if (applicableRules.Count == 0)
            {
                context.Builder.Append(number).Append(" ");
                context.AddToCount("Integer");
                return context;
            }

            StringBuilder resultBuilder = new();

            applicableRules
                .ForEach(r =>
                {
                    resultBuilder.Append(r.GenerateString());
                });

            context.AddToCount(resultBuilder.ToString());
            context.Builder.Append(resultBuilder).Append(" ");
            return context;
        }

        private bool InputsNegative(int start, int finish)
        {
            return start < 0 || finish < 0;
        }
    }

}
