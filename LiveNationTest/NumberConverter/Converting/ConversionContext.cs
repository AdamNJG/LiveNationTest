using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Converting
{
    public class ConversionContext
    {
        public StringBuilder Builder { get; init; }
        public Dictionary<string, int> Count { get; init; }

        public ConversionContext()
        {
            Builder = new StringBuilder();
            Count = new Dictionary<string, int>();
        }

        public void AddToCount(string ruleString)
        {
            if (Count.TryGetValue(ruleString, out int currentValue))
            {
                Count[ruleString] = ++currentValue;
            }
            else
            {
                Count[ruleString] = 1;
            }
        }
    }
}
