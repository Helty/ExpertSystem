using ExpertSystemCourseWork.common;
using ExpertSystemCourseWork.domain;
using ExpertSystemCourseWork.windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
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
        readonly Dictionary<VariableType, string> m_variableTypeMap = new()
        {
            { VariableType.Queried, "Запрашиваемая" },
            { VariableType.Deducted, "Выводимая" },
        };
        readonly Dictionary<string, VariableType> m_invVariableTypeMap = new()
        {
            { "Запрашиваемая", VariableType.Queried},
            { "Выводимая", VariableType.Deducted },
        };

        private ExpertSystem m_expertSystem;
        
        public MainWindow()
        {
            InitializeComponent();

            m_expertSystem = SetExpertSystem();

            UpdateApplicationLayout();
        }

        private ExpertSystem SetExpertSystem()
        {
            ExpertSystem expertSystem = new();

            string variableName = "Угроза";
            string domainName = "Угрозы";

            Domain domain = new(domainName, new List<string>()
                {
                    "УБИ.001: Угроза автоматического распространения вредоносного кода в грид-системе",
                    "УБИ.002: Угроза агрегирования данных, передаваемых в грид-системе",
                    "УБИ.003: Угроза использования слабостей криптографических алгоритмов и уязвимостей в программном обеспечении их реализации",
                    "УБИ.004: Угроза аппаратного сброса пароля BIOS",
                    "УБИ.005: Угроза внедрения вредоносного кода в BIOS",
                    "УБИ.006: Угроза внедрения кода или данных",
                    "УБИ.007: Угроза воздействия на программы с высокими привилегиями",
                    "УБИ.008: Угроза восстановления и/или повторного использования аутентификационной информации",
                    "УБИ.009: Угроза восстановления предыдущей уязвимой версии BIOS",
                    "УБИ.010: Угроза выхода процесса за пределы виртуальной машины",
                }
            );

            Variable Variable = new(
                variableName,
                domain,
                VariableType.Deducted
            );

            expertSystem.GetDomains().Add(domainName, domain);
            expertSystem.GetVariables().Add(variableName, Variable);

            return expertSystem;
        }

        private void UpdateApplicationLayout()
        {
            UpdateVariablesLayout();
        }

        #region ФАКТЫ
        private void VariablesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string variableName = ((string)((ListBox)sender).SelectedItem) ?? string.Empty;

            if (variableName == string.Empty) { return; }

            Variable variable = (Variable)m_expertSystem.GetVariables()[variableName]!;

            #region имя переменной
            VariableNameTextBox.Text = variableName;
            #endregion

            #region домен
            Domain variableDomain = variable.GetDomain();
            
            if (variableDomain == null)
            {
                DomainComboBox.SelectedIndex = -1;
            }
            else
            {
                DomainComboBox.Items.Clear();

                foreach (string domainName in m_expertSystem.GetDomainNames())
                {
                    DomainComboBox.Items.Add(domainName);
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
                    ValueComboBox.Items.Add(value);
                }

                ValueComboBox.SelectedIndex = 0;
            }
            #endregion

            #region тип переменной

            TypeComboBox.Items.Clear();
            foreach (VariableType varType in m_expertSystem.GetVariableTypes())
            {
                TypeComboBox.Items.Add(m_variableTypeMap[varType]);
            }

            VariableType variableType = variable.GetVariableType();
            TypeComboBox.SelectedItem = m_variableTypeMap[variableType];
            InputVariableQuestionTextBox.IsEnabled = (variableType == VariableType.Queried);
            #endregion

            #region вопрос
            if (variableType == VariableType.Queried)
            {
                InputVariableQuestionTextBox.Text = ((Variable)m_expertSystem.GetVariables()[variableName]!).GetQuestion();
            }
            #endregion
        }
        private void DomainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string newDomainName = ((string)((ComboBox)sender).SelectedItem) ?? string.Empty;
            if (newDomainName == string.Empty) { return; }

            Dictionary<string, Domain> domains = m_expertSystem.GetDomains();

            if (newDomainName != null && domains.Keys.Contains(newDomainName))
            {
                Domain newDomain = domains[newDomainName];
                ValueComboBox.Items.Clear();

                for (int i = 0; i < newDomain.GetValueCount(); i++)
                {
                    ValueComboBox.Items.Add(newDomain.GetValue(i));
                }
                ValueComboBox.SelectedIndex = 0;
            }
        }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string variableType = ((string)((ComboBox)sender).SelectedItem) ?? string.Empty;
            if (variableType == string.Empty) { return; }

            InputVariableQuestionTextBox.IsEnabled = (m_invVariableTypeMap[variableType] == VariableType.Queried);
            InputVariableQuestionTextBox.Clear();
        }

        private void AddNewDomainButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDomain addNewDomain = new(m_expertSystem);
            if (addNewDomain.ShowDialog() == false)
            {
                m_expertSystem = addNewDomain.ReturnExpertSystem;
            }
            UpdateDomainLayout();
        }
        private void AddNewValueButton_Click(object sender, RoutedEventArgs e)
        {
            string domainNameToChange = DomainComboBox.SelectedItem.ToString()!;

            if (domainNameToChange != string.Empty)
            {
                AddNewValueDomain addNewValueDomain = new(m_expertSystem, domainNameToChange);
                if (addNewValueDomain.ShowDialog() == false)
                {
                    m_expertSystem = addNewValueDomain.ReturnExpertSystem;
                }

                UpdateDomainValuesLayout(domainNameToChange);
            }
            else
            {
                MessageBox.Show("Имя домена к которому будет добавляться значение не задан.");
            }
        }

        private void AddNewVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string newVariableName = VariableNameTextBox.Text;

            if (!m_expertSystem.GetVariables().Keys.Contains(newVariableName))
            {
                Variable? newVariable = CreateVariableWithCurrentValue(newVariableName);
                if (newVariable == null) return;

                m_expertSystem.GetVariables().Add(newVariableName, newVariable);
                UpdateVariablesLayout();

                MessageBox.Show($"Переменная с именем '{newVariableName}' успешно добавлена.");
            }
            else
            {
                MessageBox.Show($"Переменная с именем '{newVariableName}' уже существует.");
            }
        }
        private void SaveVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string? oldVariableName = VariablesListBox.SelectedItem.ToString();
            string? newVariableName = VariableNameTextBox.Text;

            if (newVariableName != null && m_expertSystem.GetVariables().Keys.Contains(oldVariableName!))
            {
                Variable? newVariable = CreateVariableWithCurrentValue(newVariableName);
                if (newVariable == null) return;

                if (newVariableName == oldVariableName)
                {
                    m_expertSystem.GetVariables()[newVariableName] = newVariable;
                }
                else
                {
                    m_expertSystem.GetVariables().Remove(oldVariableName!);
                    m_expertSystem.GetVariables().Add(newVariableName, newVariable);
                }

                UpdateVariablesLayout();
                MessageBox.Show($"Переменная успешно изменена.");
            }
            else
            {
                MessageBox.Show($"Переменную с именем '{oldVariableName}' не удалось изменить.");
            }
        }
        private void DeleteVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string? variableNameToDelete = VariablesListBox.SelectedItem.ToString();
            if (variableNameToDelete != null && m_expertSystem.GetVariables().Keys.Contains(variableNameToDelete))
            {
                m_expertSystem.GetVariables().Remove(variableNameToDelete);
                UpdateVariablesLayout();
                MessageBox.Show($"Переменная с именен: '{variableNameToDelete}' успешно удалена.");
            }
            else
            {
                MessageBox.Show($"Переменная с именен: '{variableNameToDelete}' не найдена для удаления.");
            }
        }

        private Variable? CreateVariableWithCurrentValue(string newVariableName)
        {
            string? domainName = DomainComboBox.SelectedValue.ToString();
            if (domainName == null)
            {
                MessageBox.Show($"У переменной '{newVariableName}' не установлен домен co значениями.");
                return null;
            }

            Variable newVariable = new(
                newVariableName,
                (Domain)m_expertSystem.GetDomains()[domainName]!,
                m_invVariableTypeMap[TypeComboBox.SelectedValue.ToString()!],
                InputVariableQuestionTextBox.Text
            );

            return newVariable;
        }

        private void UpdateVariablesLayout()
        {
            VariablesListBox.Items.Clear();
            foreach (string variableName in m_expertSystem.GetVariables().Keys)
            {
                VariablesListBox.Items.Add(variableName);
            }
            VariablesListBox.SelectedIndex = 0;
        }
        private void UpdateDomainValuesLayout(string domainName)
        {
            ValueComboBox.Items.Clear();
            foreach (string valueName in m_expertSystem.GetDomains()[domainName].GetValueList())
            {
                ValueComboBox.Items.Add(valueName);
            }
            ValueComboBox.SelectedIndex = 0;
        }
        private void UpdateDomainLayout()
        {
            DomainComboBox.Items.Clear();
            foreach (string domainName in m_expertSystem.GetDomains().Keys)
            {
                DomainComboBox.Items.Add(domainName);
            }
            DomainComboBox.SelectedIndex = 0;
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
