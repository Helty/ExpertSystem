using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystemCourseWork.common
{
    public class DomainException : Exception
    {
        public DomainException()
            : base()
        {
        }

        public DomainException(string msg)
            : base(msg)
        {
        }
    }

    public class RuleException : Exception
    {
        public RuleException()
            : base()
        {
        }

        public RuleException(string msg)
            : base(msg)
        {
        }
    }

    public class VariableException : Exception
    {
        public VariableException()
            : base()
        {
        }

        public VariableException(string msg)
            : base(msg)
        {
        }
    }
}
