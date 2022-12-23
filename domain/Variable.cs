using System;
using ExpertSystemCourseWork.common;

namespace ExpertSystemCourseWork.domain
{
    public class Variable
    {
        private string m_name;
        private Domain m_domain;
        private VariableType m_variableType;
        private string m_question;

        public Variable()
        {
            m_name = string.Empty;
            m_domain = new Domain();
            m_variableType = VariableType.Queried;
            m_question = string.Empty;
        }

        public Variable(string name, Domain domain, VariableType variableType = VariableType.Queried, string question = "")
        {
            m_name = name;
            m_domain = domain;
            m_variableType = variableType;
            m_question = question;
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

        public Domain GetDomain()
        {
            return m_domain;
        }

        public void SetDomain(Domain newDomain)
        {
            if (newDomain != m_domain)
            {
                m_domain = newDomain;
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

        public int CompareTo(Variable variable)
        {
            return m_name.CompareTo(variable.GetName());
        }

        public override string ToString()
        {
            return m_name + " (" + m_domain.GetName() + ")";
        }
    }
}
