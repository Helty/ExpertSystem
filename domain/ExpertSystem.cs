using ExpertSystem.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace ExpertSystem.domain
{
    public class ExpertSystem
    {
        private OrderedDictionary<string, ValueDomain> m_valueDomains = new OrderedDictionary<string, ValueDomain>();
        private OrderedDictionary<string, Variable> m_variables = new OrderedDictionary<string, Variable>();
        private OrderedDictionary<string, Rule> m_rules = new OrderedDictionary<string, Rule>();

        private Variable m_goal;

        private List<Fact> m_provedFacts = new();
        private List<Rule> m_workedRules = new();
        public string m_result = "";

        public List<Rule> GetWorkedRules()
        {
            return m_workedRules;
        }
        public List<Fact> GetProvedFacts()
        {
            return m_provedFacts;
        }

        public OrderedDictionary<string, ValueDomain> GetValueDomains()
        {
            return m_valueDomains;
        }
        public void SetValueDomains(OrderedDictionary<string, ValueDomain> newValueDomains)
        {
            if (newValueDomains != m_valueDomains)
            {
                m_valueDomains = newValueDomains;
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

        public RightlyType DoRule(Rule rule)
        {
            bool isFactTrue = true;
            foreach (Fact reasonFact in rule.GetReasons())
            {
                if (!Fact.ContainsIn(reasonFact, m_provedFacts.ToArray()))
                {
                    Fact fact = Consult(reasonFact.GetVariable());
                    m_provedFacts.Add(fact);

                    isFactTrue = (fact.GetRightlyType() == RightlyType.Yes) 
                        ? (reasonFact.CompareTo(fact) == 0) 
                        : false;

                    foreach (string s in fact.GetVariable().GetDomainValue().GetValueList())
                    {
                        if (s != fact.GetWeight())
                        {
                            m_provedFacts.Add(new Fact(fact.GetVariable(), s, RightlyType.No));
                        }
                    }

                    if (!isFactTrue) break;
                }
                else
                {

                    isFactTrue = (Fact.GetFromMas(reasonFact, m_provedFacts.ToArray()).GetRightlyType() == RightlyType.Yes);
                    if (!isFactTrue) break;
                }
            }

            if (isFactTrue) // если все верно - делаем вывод
            {
                if (rule.GetResult() == null || !rule.GetResult().GetVariable().GetDomainValue().GetValueList().Contains(rule.GetResult().GetWeight()))
                {
                    throw new DomainException("Правило " + rule.GetName() + " пытается присвоить значение не из домена!");
                }
                rule.GetResult().SetRightlyType(RightlyType.Yes);
                m_provedFacts.Add(rule.GetResult());
                rule.m_worked = RuleWorkType.Signifi;
                m_workedRules.Add(rule);
                return RightlyType.Yes;
            }
            else
            {
                rule.SetWorkedType(RuleWorkType.Unsignify);
                return RightlyType.Unknown;
            }
        }

        private Fact Consult(Variable goal)
        {
            if (goal.GetDomainValue() == null)
            {
                throw new DomainException("У переменной \"" + goal.GetName() + "\" неизвестен домен!");
            }
            if (goal.GetDomainValue().GetValueList().Count == 0)
            {
                throw new DomainException("Домен \"" + goal.GetDomainValue().GetName() + "\" не имеет значений!");
            }

            if (goal.GetVariableType() == VariableType.Queried)
            {
            }
            else
            {
                foreach (string s in m_rules.GetKeys()) // если выводимая
                {
                    if (m_rules[s].GetResult() != null && m_rules[s].GetResult().GetVariable().CompareTo(goal) == 0)
                    {
                        switch (DoRule(m_rules[s]))
                        {
                            case RightlyType.Unknown:
                                if (goal.GetVariableType() == VariableType.DeductionQueried) // выводимо-запрашиваемая
                                {
                                }
                                continue;
                            default:
                                return m_rules[s].GetResult();
                        }
                    }
                }
            }

            return new Fact(goal, goal.GetDomainValue().GetValue(0), RightlyType.Unknown);
        }
    }
}
