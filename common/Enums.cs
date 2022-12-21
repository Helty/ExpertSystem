using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem.common
{
    [Serializable]
    public enum RuleWorkType
    {
        No,
        Signifi,
        Unsignify
    }

    [Serializable]
    public enum RightlyType
    {
        Unknown = 1,
        Yes,
        No
    }

    [Serializable]
    public enum VariableType
    {
        Deducted,
        Queried,
        DeductionQueried
    }
}
