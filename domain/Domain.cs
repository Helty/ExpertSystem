using ExpertSystemCourseWork.common;
using System.Collections.Generic;

namespace ExpertSystemCourseWork.domain
{
    public class Domain
    {
        private string m_name;
        private List<string> m_valueList;

        public Domain()
        {
            m_name = string.Empty;
            m_valueList = new List<string>();
        }

        public Domain(string name, List<string> values)
        {
            m_name = name;
            m_valueList = values;
        }

        public List<string> GetValueList()
        {
            m_valueList.Sort();
            return m_valueList;
        }

        public int GetValueCount()
        {
            return m_valueList.Count;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetValue(int position)
        {
            if ((position > m_valueList.Count - 1) || (position < 0))
            {
                throw new DomainException("Индекс находился вне границ списка значений");
            }
            return m_valueList[position];
        }

        public void Move(int oldPosition, int newPosition)
        {
            if ((oldPosition > m_valueList.Count - 1) ||
                (newPosition > m_valueList.Count - 1) ||
                (oldPosition < 0) || (newPosition < 0))
            {
                throw new DomainException("Индекс находился вне границ списка значений");
            }

            string oldValue = m_valueList[oldPosition];
            if (oldPosition > newPosition)
            {
                for (int i = oldPosition; i > newPosition; i--)
                {
                    m_valueList[i] = m_valueList[i - 1];
                }
            }
            else
            {
                for (int i = oldPosition; i < newPosition; i++)
                {
                    m_valueList[i] = m_valueList[i + 1];
                }
            }
            m_valueList[newPosition] = oldValue;
        }

        public bool IsExistValue(string value)
        {
            return m_valueList.Contains(value);
        }

        public override string ToString()
        {
            string result = "";
            if (m_valueList.Count > 0)
            {
                for (int i = 0; i < m_valueList.Count - 1; i++)
                {
                    result += m_valueList[i] + " ";
                }
                result += m_valueList[m_valueList.Count - 1];
            }
            else
            {
                result = "Empty";
            }
            return result;
        }
    }
}
