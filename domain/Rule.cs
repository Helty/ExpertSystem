﻿using System.Collections.Generic;

using ExpertSystem.common;

namespace ExpertSystem.domain
{
    public class Rule
    {
        private List<Fact> m_reasons = new();
        public RuleWorkType m_worked;

        public string? m_name;
        public Fact? m_result;
        public string? m_reasoning;

        #region Конструктор
        public Rule(string name)
        {
            m_name = name;
            m_worked = RuleWorkType.No;
        }

        public Rule(Fact reason, Fact result)
        {
            m_reasons.Add(reason);
            m_result = result;
            m_worked = RuleWorkType.No;
        }
        #endregion

        public List<Fact> GetReasons()
        {
            return m_reasons;
        }
        public void SetReasons(List<Fact> newReasons)
        {
            if (newReasons != m_reasons)
            {
                m_reasons = newReasons;
            }
        }

        public RuleWorkType GetWorkedType()
        {
            return m_worked;
        }
        public void SetWorkedType(RuleWorkType newWorkedType)
        {
            if (newWorkedType != m_worked)
            {
                m_worked = newWorkedType;
            }
        }

        public string? GetName()
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

        public Fact GetResult()
        {
            return m_result;
        }
        public void SetResult(Fact newResult)
        {
            if (newResult != m_result)
            {
                m_result = newResult;
            }
        }

        public string? GetReasoning()
        {
            return m_reasoning;
        }
        public void SetReasoning(string newReasoning)
        {
            if (newReasoning != m_reasoning)
            {
                m_reasoning = newReasoning;
            }
        }

        public Fact GetReason(int position)
        {
            if ((position > m_reasons.Count - 1) || (position < 0))
            {
                throw new RuleException("Индекс находился вне границ списка посылок");
            }
            return m_reasons[position];
        }

        public void InsertReason(Fact fact, int position)
        {
            if (!Contains(fact))
            {
                if ((position < 0) || (position > m_reasons.Count))
                {
                    throw new RuleException("Индекс находился вне границ списка посылок");
                }

                m_reasons.Add(fact);
                Move(m_reasons.Count - 1, position);
            }
        }

        public int ReasonCount()
        {
            return m_reasons.Count;
        }

        public void Move(int oldPosition, int newPosition)
        {
            if ((oldPosition > m_reasons.Count - 1) || 
                (newPosition > m_reasons.Count - 1) || 
                (oldPosition < 0) || (newPosition < 0))
            {
                throw new RuleException("Индекс находился вне границ списка посылок");
            }

            Fact oldReason = m_reasons[oldPosition];
            if (oldPosition > newPosition)
            {
                for (int i = oldPosition; i > newPosition; i--)
                {
                    m_reasons[i] = m_reasons[i - 1];
                }
            }
            else
            {
                for (int i = oldPosition; i < newPosition; i++)
                {
                    m_reasons[oldPosition] = m_reasons[i + 1];
                }
            }
            m_reasons[newPosition] = oldReason;
        }
        public void Move(Fact fact, int newPosition)
        {
            if (!Contains(fact))
            {
                throw new RuleException("Запрашиваемая посылка не найдена");
            }
            Move(IndexOf(fact), newPosition);
        }

        public void Remove(int position)
        {
            if ((position > m_reasons.Count - 1) || (position < 0))
            {
                throw new RuleException("Индекс находился вне границ списка посылок");
            }

            if (m_reasons.Count == 1)
            {
                throw new RuleException("Попытка удалить едиственную посылку в правиле");
            }

            m_reasons.RemoveAt(position);
        }
        public void Remove(Fact fact)
        {
            if (!Contains(fact))
            {
                if (m_reasons.Count == 1)
                {
                    throw new RuleException("Попытка удалить едиственную посылку в правиле");
                }
                m_reasons.RemoveAt(IndexOf(fact));
            }
        }

        public bool Contains(Fact fact)
        {
            foreach (Fact f in m_reasons)
            {
                if (f.CompareTo(fact) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(Fact fact)
        {
            for (int reasonPos = 0; reasonPos < m_reasons.Count; reasonPos++)
            {
                if (m_reasons[reasonPos].CompareTo(fact) == 0)
                    return reasonPos;
            }
            return -1;
        }

        public override string ToString()
        {
            if (m_reasons.Count > 0)
            {
                string start = "ЕСЛИ ";
                for (int i = 0; i < m_reasons.Count - 1; i++)
                {
                    start += "(" + m_reasons[i].ToString() + ") И ";
                }
                start += "(" + m_reasons[m_reasons.Count - 1] + ") ТОГДА ";
                if (m_result != null)
                {
                    start += m_result.ToString();
                }
                return start;
            }
            else
            {
                return "";
            }
        }
    }
}
