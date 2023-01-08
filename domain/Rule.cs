using ExpertSystemCourseWork.common;
using System.Collections.Generic;

namespace ExpertSystemCourseWork.domain
{
    public class Rule
    {
        private string m_name;
        private string m_argumentation;
        private List<Fact> m_causes;
        private Fact? m_result;
        private RuleWorkType m_workType;

        public Rule()
        {
            m_name = string.Empty;
            m_argumentation = string.Empty;

            m_causes = new();
            m_result = null;
            m_workType = RuleWorkType.No;
        }

        public Rule(string name, string argumentation, List<Fact> causes, Fact result)
        {
            m_name = name;
            m_argumentation = argumentation;
            m_causes = causes;
            m_result = result;
            m_workType = RuleWorkType.No;
        }

        public List<Fact> GetCauses()
        {
            return m_causes;
        }

        public Fact GetCause(int position)
        {
            if ((position > m_causes.Count - 1) || (position < 0))
            {
                throw new RuleException("Индекс находился вне границ списка посылок");
            }
            return m_causes[position];
        }

        public RuleWorkType GetWorkedType()
        {
            return m_workType;
        }

        public void SetWorkedType(RuleWorkType newWorkedType)
        {
            if (newWorkedType != m_workType)
            {
                m_workType = newWorkedType;
            }
        }

        public string GetRuleName()
        {
            return m_name;
        }

        public void SetName(string newName)
        {
            if (newName != m_name)
            {
                m_name = newName;
            }
        }

        public Fact? GetResult()
        {
            return m_result;
        }

        public void SetResult(Fact? newResult)
        {
            if (newResult != m_result)
            {
                m_result = newResult;
            }
        }

        public string GetArgumentation()
        {
            return m_argumentation;
        }

        public void SetArgumentation(string newArgumentation)
        {
            if (newArgumentation != m_argumentation)
            {
                m_argumentation = newArgumentation;
            }
        }

        public void InsertCause(Fact fact, int position)
        {
            if (!IsFactContainsInCauses(fact))
            {
                if ((position < 0) || (position > m_causes.Count))
                {
                    throw new RuleException("Индекс находился вне границ списка посылок");
                }

                m_causes.Add(fact);
                Move(m_causes.Count - 1, position);
            }
        }

        public int CausesCount()
        {
            return m_causes.Count;
        }

        public override string ToString()
        {
            if (m_causes.Count > 0)
            {
                string start = m_name + ": ЕСЛИ ";
                for (int i = 0; i < m_causes.Count - 1; i++)
                {
                    start += "(" + m_causes[i].ToString() + ") И ";
                }
                start += "(" + m_causes[m_causes.Count - 1] + ") ТОГДА ";
                if (m_result != null)
                {
                    start += m_result.ToString();
                }
                return start;
            }
            else
            {
                return m_name + ": ";
            }
        }

        private bool IsFactContainsInCauses(Fact fact)
        {
            foreach (Fact f in m_causes)
            {
                if (f.CompareTo(fact) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void Move(int oldPosition, int newPosition)
        {
            if ((oldPosition > m_causes.Count - 1) ||
                (newPosition > m_causes.Count - 1) ||
                (oldPosition < 0) || (newPosition < 0))
            {
                throw new RuleException("Индекс находился вне границ списка посылок");
            }

            Fact oldReason = m_causes[oldPosition];
            if (oldPosition > newPosition)
            {
                for (int i = oldPosition; i > newPosition; i--)
                {
                    m_causes[i] = m_causes[i - 1];
                }
            }
            else
            {
                for (int i = oldPosition; i < newPosition; i++)
                {
                    m_causes[oldPosition] = m_causes[i + 1];
                }
            }
            m_causes[newPosition] = oldReason;
        }
    }
}
