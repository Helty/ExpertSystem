using System.Collections.Generic;

using ExpertSystem.common;

namespace ExpertSystem.domain
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

        public List<string> GetValueList()
        {
            return m_valueList;
        }

        public void SetValueList(List<string> newValueList)
        {
            if (newValueList != m_valueList)
            {
                m_valueList = newValueList;
            }
        }

        public int GetValueCount()
        {
            return m_valueList.Count;
        }

        public string GetName()
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

        public string GetValue(int position)
        {
            if ((position > m_valueList.Count - 1) || (position < 0))
            {
                throw new DomainException("Индекс находился вне границ списка значений");
            }
            return m_valueList[position];
        }

        public void InsertValue(string value, int position)
        {
            if (!m_valueList.Contains(value))
            {
                if ((position > m_valueList.Count) || (position < 0))
                {
                    throw new DomainException("Индекс находился вне границ списка значений");
                }

                m_valueList.Add(value);
                Move(m_valueList.Count - 1, position);
            }
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

        public void Move(string value, int newPosition)
        {
            if (!m_valueList.Contains(value))
            {
                throw new DomainException("Список значений не содержит требуемого элемента");
            }
            Move(m_valueList.IndexOf(value), newPosition);
        }

        public void Remove(int position)
        {
            if ((position > m_valueList.Count - 1) || (position < 0))
            {
                throw new DomainException("Индекс находился вне границ списка значений");
            }
            m_valueList.RemoveAt(position);
        }

        public void Remove(string value)
        {
            if (!m_valueList.Contains(value))
            {
                throw new DomainException("Список значений не содержит требуемого элемента");
            }
            m_valueList.Remove(value);
        }

        public bool IsExistValue(string value)
        {
            return m_valueList.Contains(value);
        }

        public int IndexOf(string value)
        {
            return m_valueList.IndexOf(value);
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
