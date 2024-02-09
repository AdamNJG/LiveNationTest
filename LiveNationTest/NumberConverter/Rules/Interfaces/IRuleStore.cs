using NumberConverter.Rules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberConverter.Rules.Interfaces
{
    public interface IRuleStore
    {
        void StoreRule(Rule rule);
    }
}
