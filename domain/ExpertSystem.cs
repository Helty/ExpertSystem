using ExpertSystemCourseWork.common;
using System.Collections.Generic;

namespace ExpertSystemCourseWork.domain
{
    public class ExpertSystem
    {
        private readonly List<VariableType> m_variableTypes = new()
        {
            VariableType.Queried,
            VariableType.Deducted,
        };

        private Dictionary<string, Domain> m_domains;
        private Dictionary<string, Variable> m_variables;
        private Dictionary<string, Rule> m_rules;

        private List<Fact> m_provedFacts;
        private List<Rule> m_workedRules;

        private Variable m_target;

        public ExpertSystem()
        {
            m_domains = new();
            m_variables = new();
            m_rules = new();

            m_provedFacts = new List<Fact>();
            m_workedRules = new List<Rule>();

            m_target = new Variable();
        }

        public List<Rule> GetWorkedRules()
        {
            return m_workedRules;
        }
        public List<Fact> GetProvedFacts()
        {
            return m_provedFacts;
        }
        public List<VariableType> GetVariableTypes()
        {
            return m_variableTypes;
        }
        public List<string> GetDomainNames()
        {
            List<string> result = new();

            foreach (string domainName in m_domains.Keys)
            {
                result.Add(domainName);
            }

            return result;
        }

        public Variable GetTarget()
        {
            return m_target;
        }
        public void SetTarget(Variable newVariable)
        {
            if (newVariable != m_target)
            {
                m_target = newVariable;
            }
        }

        public Dictionary<string, Domain> GetDomains()
        {
            return m_domains;
        }
        public void SetDomains(Dictionary<string, Domain> newDomains)
        {
            if (newDomains != m_domains)
            {
                m_domains = newDomains;
            }
        }

        public Dictionary<string, Variable> GetVariables()
        {
            return m_variables;
        }
        public void SetVariables(Dictionary<string, Variable> newVariables)
        {
            if (newVariables != m_variables)
            {
                m_variables = newVariables;
            }
        }

        public Dictionary<string, Rule> GetRules()
        {
            return m_rules;
        }
        public void SetRules(Dictionary<string, Rule> newRules)
        {
            if (newRules != m_rules)
            {
                m_rules = newRules;
            }
        }

        public void ClearProvedFacts()
        {
            m_provedFacts.Clear();
        }
        public void ClearWorkedRules()
        {
            m_workedRules.Clear();
        }
    }
}