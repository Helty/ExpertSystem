using ExpertSystemCourseWork.common;
using ExpertSystemCourseWork.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExpertSystemCourseWork.windows.rules
{
    public partial class AddNewFact : Window
    {
        private Fact? m_fact;
        private ExpertSystem m_expertSystem;

        public Fact? ReturnNewFact
        {
            get { return m_fact; }
        }

        public AddNewFact(ExpertSystem expertSystem, bool allRules)
        {
            InitializeComponent();

            m_expertSystem = expertSystem;

            foreach (string variableName in m_expertSystem.GetVariables().Keys)
            {
                if (allRules || m_expertSystem.GetVariables()[variableName].GetVariableType() != VariableType.Queried)
                {
                    VariablesComboBox.Items.Add(variableName);
                }
            }

            VariablesComboBox.SelectedIndex = 0;
            ValuesComboBox.SelectedIndex = 0;
        }

        public AddNewFact(ExpertSystem expertSystem, string variableName, string valueName)
        {
            InitializeComponent();

            m_expertSystem = expertSystem;

            foreach (string varName in m_expertSystem.GetVariables().Keys)
            {
                VariablesComboBox.Items.Add(varName);
            }

            VariablesComboBox.Text = variableName;
            ValuesComboBox.Text = valueName;
        }

        private void VariablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValuesComboBox.Items.Clear();
            string selectedVariableName = VariablesComboBox.SelectedItem.ToString()!;
            if (VariablesComboBox.SelectedIndex >= 0 && selectedVariableName != string.Empty)
            {
                Domain domain = m_expertSystem.GetVariables()[selectedVariableName].GetDomain();
                for (int i = 0; i < domain.GetValueCount(); i++)
                {
                    ValuesComboBox.Items.Add(domain.GetValue(i));
                }
            }
        }

        private void AddNewFactButton_Click(object sender, RoutedEventArgs e)
        {
            if (VariablesComboBox.SelectedItem != null && ValuesComboBox.SelectedItem != null)
            {
                Fact fact = new();
                fact.SetVariable(m_expertSystem.GetVariables()[VariablesComboBox.SelectedItem.ToString()!]);
                fact.SetValue(ValuesComboBox.SelectedItem.ToString()!);
                m_fact = fact;
                Close();
            }
            else
            {
                MessageBox.Show("Небыли присвоены значения переменной.");
            }
        }
    }
}
