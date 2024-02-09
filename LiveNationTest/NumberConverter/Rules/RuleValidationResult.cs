using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Rules
{
    public readonly record struct RuleValidationResult(bool Success, string Message, Rule? Rule);
}
