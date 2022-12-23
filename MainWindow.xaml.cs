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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ExpertSystemCourseWork
{
    public partial class MainWindow : Window
    {
        private ExpertSystem m_expertSystem;
        
        public MainWindow()
        {
            InitializeComponent();

            m_expertSystem = SetExpertSystem();

            SetApplicationLayout();
        }

        private ExpertSystem SetExpertSystem()
        {
            return new ExpertSystem();
        }

        private void SetApplicationLayout()
        {
            SetVariablesLayout();
            //ClearRulesLayout();
            //ClearWorkedRules();
            //ClearUserInteraction();

            //UpdateVriables();
            //UpdateRules();
        }

        private void SetVariablesLayout()
        {
            VariablesListBox.Tag = null;

            string newVariableName = "Угроза";
            string[] list = { "Дa", "Нет" };
            Domain domain = new Domain("Да-Нет", list.ToList());
            Variable newVariable = new Variable(newVariableName, domain, VariableType.Queried);
            m_expertSystem.GetVariables().Add(newVariableName, newVariable);

            foreach (string variableName in m_expertSystem.GetVariables().GetKeys())
            {
                VariablesListBox.Items.Add(variableName);
            }
        }

        #region ФАКТЫ
        private void VariablesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (VariablesListBox.Tag != null)
            {
                SaveVariable(sender, e);
            }
            */

            string variableName = VariablesListBox.SelectedItem.ToString() ?? string.Empty;

            #region имя переменной
            VariableNameTextBox.Text = variableName;
            #endregion

            #region домен
            Domain variableDomain = m_expertSystem.GetVariables()[variableName].GetDomain();
            
            if (variableDomain == null)
            {
                DomainComboBox.SelectedIndex = -1;
            }
            else
            {
                if (DomainComboBox.Items.Count == 0)
                {
                    foreach (string domain in variableDomain.GetValueList())
                    {
                        DomainComboBox.Items.Add(domain.ToString());
                    }
                }

                for (int domain = 0; domain < DomainComboBox.Items.Count; domain++)
                {
                    if (variableDomain.GetName() == DomainComboBox.Items[domain].ToString())
                    {
                        DomainComboBox.SelectedIndex = domain;
                        break;
                    }
                }
            }
            #endregion

            #region значения
            List<string> values = variableDomain!.GetValueList();

            if (values.Count == 0)
            {
                ValueComboBox.SelectedIndex = -1;
            }
            else
            {
                ValueComboBox.Items.Clear();

                foreach (string value in values)
                {
                    ValueComboBox.Items.Add(value.ToString());
                }

                ValueComboBox.SelectedIndex = 0;
            }
            #endregion

            #region тип переменной
            switch (m_expertSystem.GetVariables()[variableName].GetVariableType())
            {
                case VariableType.Deducted:
                    {
                        TypeComboBox.SelectedItem = VariableType.Deducted.ToString();
                        InputVariableQuestionTextBox.IsEnabled = false;
                        break;
                    }
                case VariableType.Queried:
                    {
                        TypeComboBox.SelectedItem = VariableType.Queried.ToString();
                        InputVariableQuestionTextBox.IsEnabled = true;
                        break;
                    }
            }
            #endregion

            #region вопрос
            if (m_expertSystem.GetVariables()[variableName].GetVariableType() == VariableType.Queried)
            {
                InputVariableQuestionTextBox.Text = m_expertSystem.GetVariables()[variableName].GetQuestion();
            }
            #endregion

            VariablesListBox.Tag = VariablesListBox.SelectedIndex;
        }

        private void DomainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? newDomainName = DomainComboBox.SelectedItem.ToString();
            OrderedDictionary<string, Domain> domainMap = m_expertSystem.GetDomains();

            if (newDomainName != null && domainMap.ContainsKey(newDomainName))
            {
                Domain newDomain = domainMap[newDomainName];
                ValueComboBox.Items.Clear();

                for (int i = 0; i < newDomain.GetValueCount(); i++)
                {
                    ValueComboBox.Items.Add(newDomain.GetValue(i));
                }
            }
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VariableType variableType;
            Enum.TryParse(TypeComboBox.SelectedItem.ToString(), out variableType);
            
            if (variableType == VariableType.Queried)
            {
                InputVariableQuestionTextBox.IsEnabled = true;
            }
            else
            {
                InputVariableQuestionTextBox.Clear();
                InputVariableQuestionTextBox.IsEnabled = false;
            }
        }

        private void AddNewDomainButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewValueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewVariableButton_Click(object sender, RoutedEventArgs e)
        {
            Variable newVariable = new Variable();

            if (!VariablesListBox.Items.Contains(VariableNameTextBox.Text))
            {
                newVariable.SetName(VariableNameTextBox.Text);

                string? domainName = DomainComboBox.SelectedValue.ToString();
                if (domainName == null) MessageBox.Show("У переменной не установлен домен.");
                newVariable.SetDomain(m_expertSystem.GetDomains()[domainName!]);

                VariableType variableType;
                Enum.TryParse(TypeComboBox.SelectedValue.ToString(), out variableType);
                newVariable.SetVariableType(variableType);

                string? question = InputVariableQuestionTextBox.Text;
                if (question != null) newVariable.SetQuestion(question);

                m_expertSystem.GetVariables().Add(VariableNameTextBox.Text, newVariable);
            }
            else
            {
                MessageBox.Show("Переменная с таким именем уже существует.");
            }
        }

        private void SaveVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string? variableNameToSave = VariableNameTextBox.Text;
            if (variableNameToSave != null && VariablesListBox.Items.Contains(variableNameToSave))
            {
                m_expertSystem.GetVariables()[VariablesListBox.SelectedItem.ToString()!].SetName(variableNameToSave);

                string? domainName = DomainComboBox.SelectedValue.ToString();
                if (domainName == null) MessageBox.Show("У переменной не установлен домен.");
                m_expertSystem.GetVariables()[variableNameToSave].SetDomain(m_expertSystem.GetDomains()[domainName!]);

                VariableType variableType;
                Enum.TryParse(TypeComboBox.SelectedValue.ToString(), out variableType);
                m_expertSystem.GetVariables()[variableNameToSave].SetVariableType(variableType);

                string? question = InputVariableQuestionTextBox.Text;
                if (question != null) m_expertSystem.GetVariables()[variableNameToSave].SetQuestion(question);
            }
            else
            {
                MessageBox.Show("Переменной с таким именем не существует для изменения.");
            }
        }

        private void DeleteVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string? variableNameToDelete = VariableNameTextBox.Text;
            if (variableNameToDelete != null && VariablesListBox.Items.Contains(variableNameToDelete))
            {
                m_expertSystem.GetVariables().Remove(variableNameToDelete);
            }
            else
            {
                MessageBox.Show("Нет переменной для удаления.");
            }
        }
        #endregion

        #region ПРАВИЛА
        private void AddRuleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeRuleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #region ЕСЛИ
        private void AddCousesFactButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeCousesFactButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCousesFactButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region TO
        private void InstallConsequenceRuleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteConsequenceRuleButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #endregion

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartConsultationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
