using ExpertSystem.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace ExpertSystem.domain
{
    public class Fact
    {
        private Variable m_variable;
        private string m_weight;
        private RightlyType m_trulyType;

        public Fact(Variable variable, string weight)
        {
            m_variable = variable;
            m_weight = weight;
            m_trulyType = RightlyType.Unknown;
        }
        public Fact(Variable variable, string weight, RightlyType trulyType)
            : this(variable, weight)
        {
            m_trulyType = trulyType;
        }

        public Variable GetVariable()
        {
            return m_variable;
        }
        public void SetVariable(Variable newVariable)
        {
            if (newVariable != m_variable)
            {
                m_variable = newVariable;
            }
        }

        public string GetWeight()
        {
            return m_weight;
        }
        public void SetWeight(string newWeight)
        {
            if (newWeight != m_weight)
            {
                if (!m_variable.GetDomainValue().InDomain(newWeight))
                {
                    throw new DomainException("Попытка присвоить переменной значение не из ее домена");
                }
                m_weight = newWeight;
                m_trulyType = RightlyType.Unknown;
            }
        }

        public RightlyType GetRightlyType()
        {
            return m_trulyType;
        }
        public void SetRightlyType(RightlyType newTrulyType)
        {
            if (newTrulyType != m_trulyType)
            {
                m_trulyType = newTrulyType;
            }
        }

        public override string ToString()
        {
            return m_variable.GetName() + " = " + m_weight;
        }

        public int CompareTo(Fact fact)
        {
            int isCompareVariable = m_variable.CompareTo(fact.GetVariable());

            return isCompareVariable != 0 
                ? isCompareVariable 
                : m_weight.CompareTo(fact.GetWeight());
        }

        internal static bool ContainsIn(Fact fact, Fact[] facts)
        {
            foreach (Fact sameFact in facts)
            {
                if (fact.CompareTo(sameFact) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        internal static Fact GetFromMas(Fact fact, Fact[] facts)
        {
            foreach (Fact sameFact in facts)
            {
                if (fact.CompareTo(sameFact) == 0)
                {
                    return sameFact;
                }
            }
            return null;
        }
    }
}
