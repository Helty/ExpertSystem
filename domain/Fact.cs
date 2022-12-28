using ExpertSystemCourseWork.common;
using System.Collections.Generic;

namespace ExpertSystemCourseWork.domain
{
    public class Fact
    {
        private Variable m_variable;
        private string m_value;
        private RightlyType m_rightlyType;

        public Fact()
        {
            m_variable = new();
            m_value = string.Empty;
            m_rightlyType = RightlyType.Unknown;
        }

        public Fact(Variable variable, string value, RightlyType rightlyType)
        {
            m_variable = variable;
            m_value = value;
            m_rightlyType = rightlyType;
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

        public string GetValue()
        {
            return m_value;
        }

        public void SetValue(string newValue)
        {
            if (newValue != m_value)
            {
                if (!m_variable.GetDomain().IsExistValue(newValue))
                {
                    throw new DomainException("Попытка присвоить переменной значение не из ее домена");
                }
                m_value = newValue;
                m_rightlyType = RightlyType.Unknown;
            }
        }

        public RightlyType GetRightlyType()
        {
            return m_rightlyType;
        }

        public void SetRightlyType(RightlyType newTrulyType)
        {
            if (newTrulyType != m_rightlyType)
            {
                m_rightlyType = newTrulyType;
            }
        }

        public override string ToString()
        {
            return m_variable.GetName() + " = " + m_value;
        }

        public int CompareTo(Fact fact)
        {
            int isCompareVariable = m_variable.CompareTo(fact.GetVariable());

            return isCompareVariable != 0
                ? isCompareVariable
                : m_value.CompareTo(fact.GetValue());
        }

        internal static bool ContainsIn(Fact fact, List<Fact> facts)
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

        internal static Fact? GetFromList(Fact fact, List<Fact> facts)
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
