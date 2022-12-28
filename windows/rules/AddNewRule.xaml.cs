using ExpertSystemCourseWork.domain;
using System.Windows;

namespace ExpertSystemCourseWork.windows.rules
{
    public partial class AddNewRule : Window
    {
        private Rule? m_oldRule = null;
        private Rule? m_newRule;

        public Rule? ReturnNewRule
        {
            get { return m_newRule; }
        }

        public AddNewRule()
        {
            InitializeComponent();
        }

        public AddNewRule(Rule rule)
        {
            InitializeComponent();

            InputNewNameRuleTextBox.Text = rule.GetRuleName();
            InputRuleExplanationTextBox.Text = rule.GetArgumentation();
            m_oldRule = rule;
        }

        private void AddNewRuleButton_Click(object sender, RoutedEventArgs e)
        {
            string ruleName = InputNewNameRuleTextBox.Text;
            if (ruleName == string.Empty)
            {
                MessageBox.Show("Не задано имя для правила.");
                return;
            }

            if (m_oldRule == null)
            {
                Rule newRule = new();
                newRule.SetName(InputNewNameRuleTextBox.Text);
                newRule.SetArgumentation(InputRuleExplanationTextBox.Text);
                m_newRule = newRule;
            }
            else
            {
                m_oldRule!.SetName(InputNewNameRuleTextBox.Text);
                m_oldRule!.SetArgumentation(InputRuleExplanationTextBox.Text);
                m_newRule = m_oldRule;
            }
            Close();
        }
    }
}
