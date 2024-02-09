using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Converting
{
    public readonly record struct ConverterResult(string result, Dictionary<string, int> summary);
}
