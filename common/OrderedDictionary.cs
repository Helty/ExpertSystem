using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem.common
{
    public class OrderedDictionary<TKey, TValue>
    {
        private List<TKey> m_keys = new();
        private List<TValue> m_values = new();

        #region Класс-итератор
        private class Iterator
        {
            private int m_current;
            private OrderedDictionary<TKey, TValue> m_dict;

            public Iterator(OrderedDictionary<TKey, TValue> dict)
            {
                m_dict = dict;
                m_current = -1;
            }

            public TKey GetCurrent()
            {
                return m_dict.m_keys[m_current];
            }

            public bool MoveNext()
            {
                ++m_current;
                return m_current < m_dict.m_keys.Count;
            }

            public void Reset()
            {
                m_current = -1;
            }
        }
        #endregion

        public TValue this[TKey index]
        {
            get
            {
                if (m_keys.Contains(index))
                    return m_values[m_keys.IndexOf(index)];
                else
                    throw new IndexOutOfRangeException();
            }
            set { m_values[m_keys.IndexOf(index)] = value; }
        }

        public List<TKey> GetKeys()
        {
            return m_keys;
        }

        public List<TValue> GetValues()
        {
            return m_values;
        }

        public bool Add(TKey key, TValue value)
        {
            if (m_keys.Contains(key))
            {
                return false;
            }

            m_keys.Add(key);
            m_values.Add(value);
            return true;
        }

        public int GetCount()
        {
            return m_keys.Count;
        }

        public void Clear()
        {
            m_keys.Clear();
            m_values.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return m_keys.Contains(key);
        }

        public bool ContainsValue(TValue value)
        {
            return m_values.Contains(value);
        }

        public bool Insert(TKey key, TValue value, int position)
        {
            if (m_keys.Contains(key))
            {
                return false;
            }

            m_keys.Insert(position, key);
            m_values.Insert(position, value);
            return true;
        }

        public bool Remove(TKey key)
        {
            if (!m_keys.Contains(key))
            {
                return false;
            }

            m_values.RemoveAt(m_keys.IndexOf(key));
            m_keys.Remove(key);
            return true;
        }

        public bool RemoveAt(int position)
        {
            if (position < 0 || position >= m_keys.Count)
            {
                return false;
            }

            m_values.RemoveAt(position);
            m_keys.RemoveAt(position);
            return true;
        }
    }
}
