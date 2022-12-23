using ExpertSystemCourseWork.common;
using System.Collections.Generic;

namespace ExpertSystemCourseWork.domain
{
    public class ExpertSystem
    {
        private OrderedDictionary<string, Domain> m_domains;
        private OrderedDictionary<string, Variable> m_variables;
        private OrderedDictionary<string, Rule> m_rules;

        private List<Fact> m_provedFacts;
        private List<Rule> m_workedRules;

        private readonly Variable m_goal;

        public ExpertSystem()
        {
            m_domains = new OrderedDictionary<string, Domain>();
            m_variables = new OrderedDictionary<string, Variable>();
            m_rules = new OrderedDictionary<string, Rule>();

            m_provedFacts = new List<Fact>();
            m_workedRules = new List<Rule>();

            m_goal = new Variable();
        }

        public List<Rule> GetWorkedRules()
        {
            return m_workedRules;
        }
        public List<Fact> GetProvedFacts()
        {
            return m_provedFacts;
        }

        public OrderedDictionary<string, Domain> GetDomains()
        {
            return m_domains;
        }
        public void SetValueDomains(OrderedDictionary<string, Domain> newValueDomains)
        {
            if (newValueDomains != m_domains)
            {
                m_domains = newValueDomains;
            }
        }

        public OrderedDictionary<string, Variable> GetVariables()
        {
            return m_variables;
        }
        public void SetVariables(OrderedDictionary<string, Variable> newVariables)
        {
            if (newVariables != m_variables)
            {
                m_variables = newVariables;
            }
        }

        public OrderedDictionary<string, Rule> GetRules()
        {
            return m_rules;
        }
        public void SetRules(OrderedDictionary<string, Rule> newRules)
        {
            if (newRules != m_rules)
            {
                m_rules = newRules;
            }
        }

        public Fact StartConsult()
        {
            m_provedFacts = new List<Fact>();
            m_workedRules = new List<Rule>();

            foreach (Rule rule in m_rules.GetValues())
            {
                rule.SetWorkedType(RuleWorkType.No);
            }

            return Consult(m_goal);
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

            foreach (string ruleKey in m_rules.GetKeys())
            {
                Fact ruleResult = m_rules[ruleKey].GetResult();

                if (ruleResult != null && ruleResult.GetVariable().CompareTo(goal) == 0)
                {
                    if (CheckRule(m_rules[ruleKey]) == RightlyType.Unknown) continue;
                    return m_rules[ruleKey].GetResult();
                }
            }

            return new Fact(goal, goal.GetDomain().GetValue(0), RightlyType.Unknown);
        }
    }
}