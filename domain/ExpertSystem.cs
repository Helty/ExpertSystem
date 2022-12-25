using ExpertSystemCourseWork.common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

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

        private readonly Variable m_target;

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

        public Dictionary<string, Domain> GetDomains()
        {
            return m_domains;
        }

        public void SetValueDomains(Dictionary<string, Domain> newValueDomains)
        {
            if (newValueDomains != m_domains)
            {
                m_domains = newValueDomains;
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

        public List<string> GetDomainNames()
        {
            List<string> result = new();

            foreach (string domainName in m_domains.Keys) 
            {
                result.Add(domainName);
            }

            return result;
        }

        public Fact StartConsult()
        {
            m_provedFacts = new();
            m_workedRules = new();

            foreach (Rule ruleValue in m_rules.Values)
            {
                ruleValue.SetWorkedType(RuleWorkType.No);
            }

            return Consult(m_target);
        }

        public RightlyType CheckRule(Rule rule)
        {
            bool isFactTrue = true;

            foreach (Fact reasonFact in rule.GetReasons())
            {
                if (!Fact.ContainsIn(reasonFact, m_provedFacts))
                {
                    Fact fact = Consult(reasonFact.GetVariable());
                    m_provedFacts.Add(fact);

                    isFactTrue = (fact.GetRightlyType() == RightlyType.Yes) && (reasonFact.CompareTo(fact) == 0);

                    foreach (string value in fact.GetVariable().GetDomain().GetValueList())
                    {
                        if (value != fact.GetValue())
                        {
                            m_provedFacts.Add(new Fact(fact.GetVariable(), value, RightlyType.No));
                        }
                    }
                }
                else
                {
                    Fact? fact = Fact.GetFromList(reasonFact, m_provedFacts);
                    isFactTrue = (fact != null && fact.GetRightlyType() == RightlyType.Yes);
                }

                if (!isFactTrue) break;
            }

            if (isFactTrue)
            {
                Fact fact = rule.GetResult();

                if (fact == null || !fact.GetVariable().GetDomain().GetValueList().Contains(fact.GetValue()))
                {
                    throw new DomainException("Правило " + rule.GetName() + " пытается присвоить значение не из домена!");
                }

                rule.SetWorkedType(RuleWorkType.Signifi);
                m_workedRules.Add(rule);

                fact.SetRightlyType(RightlyType.Yes);
                m_provedFacts.Add(fact);

                return RightlyType.Yes;
            }

            rule.SetWorkedType(RuleWorkType.Unsignify);
            m_workedRules.Add(rule);

            return RightlyType.Unknown;
        }

        private Fact Consult(Variable goal)
        {
            if (goal.GetDomain() == null)
            {
                throw new DomainException("У переменной \"" + goal.GetName() + "\" неизвестен домен!");
            }

            if (goal.GetDomain().GetValueList().Count == 0)
            {
                throw new DomainException("Домен \"" + goal.GetDomain().GetName() + "\" не имеет значений!");
            }

            foreach (Rule rule in m_rules.Values)
            {
                Fact ruleResult = rule.GetResult();

                if (ruleResult != null && ruleResult.GetVariable().CompareTo(goal) == 0)
                {
                    if (CheckRule(rule) == RightlyType.Unknown) continue;
                    return rule.GetResult();
                }
            }

            return new Fact(goal, goal.GetDomain().GetValue(0), RightlyType.Unknown);
        }
    }
}