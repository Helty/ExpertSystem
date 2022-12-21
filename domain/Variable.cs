using System;
using ExpertSystem.common;

namespace ExpertSystem.domain
{
    public class Variable
    {
        protected string m_name;
        protected ValueDomain? m_domainValue;
        protected VariableType m_variableType;
        protected string? m_question;
        protected string? m_reasoning;

        #region Конструктор
        public Variable(string name)
        {
            m_name = name;
        }

        public Variable(string name, ValueDomain domain)
        {
            m_name = name;
            m_domainValue = domain;
        }
        #endregion

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

        public ValueDomain GetDomainValue()
        {
            return m_domainValue!;
        }
        public void SetDomainValue(ValueDomain newDomainValues)
        {
            if (newDomainValues != m_domainValue)
            {
                m_domainValue = newDomainValues;
            }
        }

        public VariableType GetVariableType()
        {
            return m_variableType;
        }
        public void SetVariableType(VariableType newVariableType)
        {
            if (newVariableType != m_variableType)
            {
                m_variableType = newVariableType;
            }
        }

        public string? GetQuestion()
        {
            return m_question;
        }
        public void SetQuestion(string newQuestion)
        {
            if (newQuestion != m_question)
            {
                m_question = newQuestion;
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

        public int CompareTo(Variable variable)
        {
            return m_name.CompareTo(variable.GetName());
        }

        public override string ToString()
        {
            return m_name + " (" + m_domainValue!.GetName() + ")";
        }
    }
}
