using ExpertSystemCourseWork.common;
using ExpertSystemCourseWork.domain;
using ExpertSystemCourseWork.windows;
using ExpertSystemCourseWork.windows.rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            string domainNameThreat0 = "Да-Нет";
            Domain domain0 = new(domainNameThreat0, new List<string>()
                {
                    "Да",
                    "Нет",
                }
            );

            string domainNameThreat1 = "Действие";
            Domain domain1 = new(domainNameThreat1, new List<string>()
                {
                    "Ждать",
                    "Перевернуть",
                    "Снимать",
                }
            );

            string domainNameThreat2 = "Вид мяса";
            Domain domain2 = new(domainNameThreat2, new List<string>()
                {
                    "Курица",
                    "Свинина",
                }
            );

            string domainNameThreat3 = "Промежуток времени";
            Domain domain3 = new(domainNameThreat3, new List<string>()
                {
                    "Только что положил",
                    "Ну минут 10",
                    "Да долго уже",
                }
            );

            string domainNameThreat4 = "Степень поджаристости";
            Domain domain4 = new(domainNameThreat4, new List<string>()
                {
                    "Совсем сырое ещё",
                    "Начинает поджариваться",
                    "Поджаристое",
                    "Уже чёрное",
                }
            );

            string domainNameThreat5 = "Количество человек";
            Domain domain5 = new(domainNameThreat5, new List<string>()
                {
                    "Меньше 5",
                    "5 или больше",
                }
            );

            expertSystem.GetDomains().Add(domainNameThreat0, domain0);
            expertSystem.GetDomains().Add(domainNameThreat1, domain1);
            expertSystem.GetDomains().Add(domainNameThreat2, domain2);
            expertSystem.GetDomains().Add(domainNameThreat3, domain3);
            expertSystem.GetDomains().Add(domainNameThreat4, domain4);
            expertSystem.GetDomains().Add(domainNameThreat5, domain5);

            string variableNameThreat1 = "Действие";
            Variable variableThreat1 = new(
                variableNameThreat1,
                domain1,
                VariableType.Deducted
            );

            string variableNameThreat2 = "Готово с одной стороны";
            Variable variableThreat2 = new(
                variableNameThreat2,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat3 = "Готово с двух сторон";
            Variable variableThreat3 = new(
                variableNameThreat3,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat4 = "Сторона готова";
            Variable variableThreat4 = new(
                variableNameThreat4,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat5 = "Уже переворачивали";
            Variable variableThreat5 = new(
                variableNameThreat5,
                domain0,
                VariableType.Queried
            );

            string variableNameThreat6 = "Курица готова";
            Variable variableThreat6 = new(
                variableNameThreat6,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat7 = "Свинина готова";
            Variable variableThreat7 = new(
                variableNameThreat7,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat8 = "Вид мяса";
            Variable variableThreat8 = new(
                variableNameThreat8,
                domain2,
                VariableType.Queried
            );

            string variableNameThreat9 = "Время жарки";
            Variable variableThreat9 = new(
                variableNameThreat9,
                domain3,
                VariableType.Queried
            );

            string variableNameThreat10 = "Степень поджаристости";
            Variable variableThreat10 = new(
                variableNameThreat10,
                domain4,
                VariableType.Queried
            );

            string variableNameThreat11 = "Готово по мнению большинства";
            Variable variableThreat11 = new(
                variableNameThreat11,
                domain0,
                VariableType.Deducted
            );

            string variableNameThreat12 = "Количество человек";
            Variable variableThreat12 = new(
                variableNameThreat12,
                domain5,
                VariableType.Queried
            );

            expertSystem.GetVariables().Add(variableNameThreat1, variableThreat1);
            expertSystem.GetVariables().Add(variableNameThreat2, variableThreat2);
            expertSystem.GetVariables().Add(variableNameThreat3, variableThreat3);
            expertSystem.GetVariables().Add(variableNameThreat4, variableThreat4);
            expertSystem.GetVariables().Add(variableNameThreat5, variableThreat5);
            expertSystem.GetVariables().Add(variableNameThreat6, variableThreat6);
            expertSystem.GetVariables().Add(variableNameThreat7, variableThreat7);
            expertSystem.GetVariables().Add(variableNameThreat8, variableThreat8);
            expertSystem.GetVariables().Add(variableNameThreat9, variableThreat9);
            expertSystem.GetVariables().Add(variableNameThreat10, variableThreat10);
            expertSystem.GetVariables().Add(variableNameThreat11, variableThreat11);
            expertSystem.GetVariables().Add(variableNameThreat12, variableThreat12);

            /*
            string variableNameThreat = "Угроза";

            string domainNameThreat = "Угрозы";
            string domainNameYesNo = "Да-Нет";

            Domain domainThreat = new(domainNameThreat, new List<string>()
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

            Domain domainYesNo = new(domainNameYesNo, new List<string>()
                {
                    "Да",
                    "Нет",
                }
            );


            Variable variableThreat = new(
                variableNameThreat,
                domainThreat,
                VariableType.Deducted
            );

            expertSystem.GetDomains().Add(domainNameThreat, domainThreat);
            expertSystem.GetDomains().Add(domainNameYesNo, domainYesNo);

            expertSystem.GetVariables().Add(variableNameThreat, variableThreat);
            */
            return expertSystem;
        }

        private void UpdateApplicationLayout()
        {
            UpdateVariablesLayout(null);
            UpdateRulesLayout(null);

            ChangeRuleButton.IsEnabled = false;
            DeleteRuleButton.IsEnabled = false;
            CausesListBox.IsEnabled = false;
            ConsequenceListBox.IsEnabled = false;
            AddCousesFactButton.IsEnabled = false;
            ChangeCousesFactButton.IsEnabled = false;
            DeleteCousesFactButton.IsEnabled = false;
            InstallConsequenceRuleButton.IsEnabled = false;
            DeleteConsequenceRuleButton.IsEnabled = false;
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
                UpdateVariablesLayout(newVariableName);

                MessageBox.Show($"Переменная с именем '{newVariableName}' успешно добавлена.");
            }
            else
            {
                MessageBox.Show($"Переменная с именем '{newVariableName}' уже существует.");
            }
        }
        private void SaveVariableButton_Click(object sender, RoutedEventArgs e)
        {
            if (VariablesListBox.SelectedItem == null)
            {
                MessageBox.Show("Нет выбранной переменной.");
                return;
            }

            string oldVariableName = VariablesListBox.SelectedItem.ToString()!;
            string? newVariableName = VariableNameTextBox.Text;

            if (newVariableName != null && m_expertSystem.GetVariables().ContainsKey(oldVariableName!))
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

                UpdateVariablesLayout(newVariableName);
            }
            else
            {
                MessageBox.Show($"Переменную с именем '{oldVariableName}' не удалось изменить.");
            }
        }
        private void DeleteVariableButton_Click(object sender, RoutedEventArgs e)
        {
            string? variableNameToDelete = VariablesListBox.SelectedItem.ToString();
            if (variableNameToDelete != null && m_expertSystem.GetVariables().ContainsKey(variableNameToDelete))
            {
                m_expertSystem.GetVariables().Remove(variableNameToDelete);
                UpdateVariablesLayout(null);
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

        private void UpdateVariablesLayout(string? selectedVariableName)
        {
            VariablesListBox.Items.Clear();
            TargetVariableComboBox.Items.Clear();

            int index = 0;
            foreach (string variableName in m_expertSystem.GetVariables().Keys)
            {
                if (variableName == selectedVariableName)
                {
                    VariablesListBox.SelectedIndex = index;
                }

                VariablesListBox.Items.Add(variableName);
                TargetVariableComboBox.Items.Add(variableName);
                index++;
            }

            if (selectedVariableName == null) VariablesListBox.SelectedIndex = 0;
            TargetVariableComboBox.SelectedIndex = -1;
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
        private void RulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRuleSelected = RulesListBox.SelectedIndex >= 0;

            InputDescriptionRuleTextBox.Clear();
            CausesListBox.Items.Clear();
            ConsequenceListBox.Items.Clear();

            CausesListBox.IsEnabled = isRuleSelected;
            ConsequenceListBox.IsEnabled = isRuleSelected;

            AddCousesFactButton.IsEnabled = isRuleSelected;
            ChangeCousesFactButton.IsEnabled = isRuleSelected;
            DeleteCousesFactButton.IsEnabled = isRuleSelected;

            InstallConsequenceRuleButton.IsEnabled = isRuleSelected;
            DeleteConsequenceRuleButton.IsEnabled = isRuleSelected;

            DeleteRuleButton.IsEnabled = isRuleSelected;
            ChangeRuleButton.IsEnabled = isRuleSelected;

            if (isRuleSelected)
            {
                string selectedRuleName = ((ListBox)sender).SelectedItem.ToString()!.Split(':')[0];
                Rule rule = m_expertSystem.GetRules()[selectedRuleName];
                RulesListBox.SelectedItem = rule.ToString();

                for (int causeIndex = 0; causeIndex < rule.CausesCount(); causeIndex++)
                {
                    CausesListBox.Items.Add(rule.GetCause(causeIndex));
                }

                if (rule.GetResult() != null)
                {
                    ConsequenceListBox.Items.Add(rule.GetResult());
                }

                InputDescriptionRuleTextBox.Text = rule.GetArgumentation();
            }
        }

        private void AddRuleButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewRule addNewRuleWindow = new AddNewRule();
            if (addNewRuleWindow.ShowDialog() == false)
            {
                Rule? newRule = addNewRuleWindow.ReturnNewRule;
                if (newRule != null && !m_expertSystem.GetRules().Values.Any(rule => rule.GetRuleName() == newRule.GetRuleName()))
                {
                    m_expertSystem.GetRules().Add(newRule.GetRuleName(), newRule);
                    UpdateRulesLayout(newRule.GetRuleName());
                }
            }
        }
        private void ChangeRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RulesListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Сначала необходимо выбрать правило");
                return;
            }

            string ruleNameToChange = RulesListBox.SelectedItem.ToString()!.Split(':')[0];
            Rule ruleToChange = m_expertSystem.GetRules()[ruleNameToChange];

            AddNewRule addNewRuleWindow = new AddNewRule(ruleToChange);
            if (addNewRuleWindow.ShowDialog() == false)
            {
                Rule? changedRule = addNewRuleWindow.ReturnNewRule;
                if (changedRule != null)
                {
                    m_expertSystem.GetRules().Remove(ruleToChange.GetRuleName());
                    m_expertSystem.GetRules().Add(changedRule!.GetRuleName(), changedRule);
                    UpdateRulesLayout(changedRule.GetRuleName());
                }
            }
        }
        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (RulesListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Сначала необходимо выбрать правило");
                return;
            }

            if (MessageBox.Show("Действительно удалить текущее правило?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string selectedRuleName = RulesListBox.SelectedItem.ToString()!;
                m_expertSystem.GetRules().Remove(selectedRuleName.Split(':')[0]);
                RulesListBox.Items.Remove(RulesListBox.SelectedItem.ToString());
                UpdateRulesLayout(null);
            }
        }

        #region ЕСЛИ
        private void AddCousesFactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddNewFact addNewFact = new AddNewFact(m_expertSystem, true);
                if (addNewFact.ShowDialog() == false)
                {
                    Fact? newFact = addNewFact.ReturnNewFact;
                    string selectedRuleName = RulesListBox.SelectedItem.ToString()!.Split(':')[0];

                    if (newFact != null && !m_expertSystem.GetRules()[selectedRuleName].GetCauses().Contains(newFact))
                    {
                        m_expertSystem.GetRules()[selectedRuleName].InsertCause(newFact, m_expertSystem.GetRules()[selectedRuleName].GetCauses().Count);
                        UpdateRulesLayout(selectedRuleName);
                    }
                }
            }
            catch (RuleException ruleException)
            {
                MessageBox.Show(ruleException.Message);
                RulesListBox.SelectedIndex = -1;
            }
        }
        private void ChangeCousesFactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CausesListBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Сначала необходимо выбрать условие-факт");
                    return;
                }

                string currentRuleName = RulesListBox.SelectedItem.ToString()!.Split(':')[0];
                string variableName = CausesListBox.SelectedItem.ToString()!.Split(" =")[0];
                string valueName = CausesListBox.SelectedItem.ToString()!.Split("= ")[1];

                AddNewFact addNewFact = new AddNewFact(m_expertSystem, variableName, valueName);

                if (addNewFact.ShowDialog() == false)
                {
                    Fact? changedFact = addNewFact.ReturnNewFact;
                    if (changedFact != null && !m_expertSystem.GetRules()[currentRuleName].GetCauses().Any(fact => (fact.GetValue() == changedFact.GetValue() && fact.GetVariable() == changedFact.GetVariable())))
                    {
                        m_expertSystem.GetRules()[currentRuleName].GetCauses().Find(fact => (fact.GetVariable().GetName() == variableName && fact.GetValue() == valueName))!.SetVariable(changedFact.GetVariable());
                        m_expertSystem.GetRules()[currentRuleName].GetCauses().Find(fact => (fact.GetVariable().GetName() == variableName && fact.GetValue() == valueName))!.SetValue(changedFact.GetValue());

                        UpdateRulesLayout(currentRuleName);
                    }
                }
            }
            catch (RuleException ruleException)
            {
                MessageBox.Show(ruleException.Message);
            }
        }
        private void DeleteCousesFactButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CausesListBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Сначала необходимо выбрать условие-факт");
                    return;
                }

                string selectedRuleName = RulesListBox.SelectedItem.ToString()!.Split(':')[0];
                string valueNameToDelete = CausesListBox.SelectedItem.ToString()!.Split("= ")[1];

                Fact fact = m_expertSystem.GetRules()[selectedRuleName].GetCauses().Find(fact => fact.GetValue() == valueNameToDelete)!;
                m_expertSystem.GetRules()[selectedRuleName].GetCauses().Remove(fact);

                UpdateRulesLayout(selectedRuleName);
            }
            catch (RuleException ruleException)
            {
                MessageBox.Show(ruleException.Message);
            }
        }
        #endregion

        #region TO
        private void InstallConsequenceRuleButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewFact addNewFact = new AddNewFact(m_expertSystem, false);
            if (addNewFact.ShowDialog() == false)
            {
                Fact? fact = addNewFact.ReturnNewFact;
                if (fact != null)
                {
                    string selectedRule = RulesListBox.SelectedItem.ToString()!.Split(':')[0];
                    m_expertSystem.GetRules()[selectedRule].SetResult(fact);

                    ConsequenceListBox.Items.Clear();
                    ConsequenceListBox.Items.Add(fact);
                    UpdateRulesLayout(selectedRule);
                }
            }
        }
        private void DeleteConsequenceRuleButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (ConsequenceListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Сначала необходимо следствие-факт в правило.");
                return;
            }

            if (MessageBox.Show("Действительно удалить вывод текущего правила?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string selectedRule = RulesListBox.SelectedItem.ToString()!.Split(':')[0];
                m_expertSystem.GetRules()[selectedRule].SetResult(null);
                UpdateRulesLayout(selectedRule);
            }
        }
        #endregion

        private void UpdateRulesLayout(string? selectedRuleName)
        {
            RulesListBox.Items.Clear();
            int index = 0;
            Rule ruleToUpdate = new();

            foreach (Rule rule in m_expertSystem.GetRules().Values.ToList())
            {
                RulesListBox.Items.Add(rule.ToString());
                if (rule.GetRuleName() == selectedRuleName)
                {
                    RulesListBox.SelectedIndex = index;
                    ruleToUpdate = rule;
                }
                index++;
            }
            if (selectedRuleName == null) RulesListBox.SelectedIndex = 0;

            UpdateRuleFactsLayout(ruleToUpdate);
        }
        private void UpdateRuleFactsLayout(Rule ruleToUpdate)
        {
            if (RulesListBox.Items.Count > 0)
            {
                if (ruleToUpdate.GetCauses().Count > 0)
                {
                    CausesListBox.Items.Clear();

                    foreach (Fact fact in ruleToUpdate.GetCauses())
                    {
                        CausesListBox.Items.Add(fact.ToString());
                    }
                    CausesListBox.SelectedIndex = 0;
                }
                else
                {
                    CausesListBox.SelectedIndex = -1;
                }

                Fact? resultfact = ruleToUpdate.GetResult();
                ConsequenceListBox.SelectedIndex = (resultfact == null || resultfact.ToString() == " = ") ? -1 : 0;
            }
        }
        private void UpdateWorkedRulesLayout()
        {
            RulesTreeView.Items.Clear();

            foreach (Rule rule in m_expertSystem.GetWorkedRules())
            {
                if (rule.GetWorkedType() == RuleWorkType.Signifi)
                {
                    var headItem = new TreeViewItem { Header = rule.GetRuleName() };

                    RulesTreeView.Items.Add(headItem);
                    var ifItem = new TreeViewItem { Header = "ЕСЛИ" };

                    headItem.Items.Add(ifItem);
                    foreach (Fact fact in rule.GetCauses())
                    {
                        ifItem.Items.Add(fact.ToString());
                    }

                    var elseItem = new TreeViewItem { Header = "ТО" };
                    headItem.Items.Add(elseItem);
                    elseItem.Items.Add(rule.GetResult()!.ToString());

                    var becauseItem = new TreeViewItem { Header = "ТАК КАК" };
                    headItem.Items.Add(becauseItem);
                    becauseItem.Items.Add(rule.GetArgumentation());
                }
            }
        }
        #endregion

        private void StartConsultationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TargetVariableComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Цель экспертизы не установлена");
                    return;
                }

                RulesTreeView.Items.Clear();
                m_expertSystem.SetTarget(m_expertSystem.GetVariables().Values.ToList().Find(var => var.GetName() == TargetVariableComboBox.SelectedValue.ToString())!);

                if (m_expertSystem.GetTarget() != null)
                {
                    Fact result = StartConsult();

                    MessageBox.Show((result.GetRightlyType() == RightlyType.Unknown)
                        ? "Экспертиза не в состоянии помочь("
                        : result.ToString()
                    );
                }
            }
            catch (DomainException de)
            {
                MessageBox.Show("Не удалось сделать вывод! Причина: " + de.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Неизвестная ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Fact StartConsult()
        {
            m_expertSystem.ClearProvedFacts();
            m_expertSystem.ClearWorkedRules();

            foreach (Rule ruleValue in m_expertSystem.GetRules().Values)
            {
                ruleValue.SetWorkedType(RuleWorkType.No);
            }

            return Consult(m_expertSystem.GetTarget());
        }
        private Fact Consult(Variable target)
        {
            if (target.GetDomain() == null)
            {
                throw new DomainException($"У переменной {target.GetName()} неизвестен домен!");
            }

            if (target.GetDomain().GetValueList().Count == 0)
            {
                throw new DomainException($"Домен {target.GetDomain().GetName()} не имеет значений!");
            }

            if (target.GetVariableType() == VariableType.Queried)
            {
                PollingWindow interviewWindow = new(target);
                if (interviewWindow.ShowDialog() == false)
                {
                    return new Fact(target, interviewWindow.ReturnUserAnswer, RightlyType.Yes);
                }
            }
            else
            {
                foreach (Rule rule in m_expertSystem.GetRules().Values)
                {
                    Fact? ruleResult = rule.GetResult();

                    if (ruleResult != null && ruleResult.GetVariable().CompareTo(target) == 0)
                    {
                        if (CheckRule(rule) == RightlyType.Unknown) continue;
                        else return rule.GetResult()!;
                    }
                }
            }

            return new Fact(target, target.GetDomain().GetValue(0), RightlyType.Unknown);
        }
        private RightlyType CheckRule(Rule rule)
        {
            bool isFactTrue = true;

            foreach (Fact causeFact in rule.GetCauses())
            {
                if (!Fact.ContainsIn(causeFact, m_expertSystem.GetProvedFacts()))
                {
                    Fact fact = Consult(causeFact.GetVariable());
                    m_expertSystem.GetProvedFacts().Add(fact);

                    isFactTrue = (fact.GetRightlyType() == RightlyType.Yes) && (causeFact.CompareTo(fact) == 0);

                    foreach (string value in fact.GetVariable().GetDomain().GetValueList())
                    {
                        if (value != fact.GetValue())
                        {
                            m_expertSystem.GetProvedFacts().Add(new Fact(fact.GetVariable(), value, RightlyType.No));
                        }
                    }
                }
                else
                {
                    Fact fact = Fact.GetFromList(causeFact, m_expertSystem.GetProvedFacts())!;
                    isFactTrue = (fact.GetRightlyType() == RightlyType.Yes);
                }

                if (!isFactTrue) break;
            }

            if (isFactTrue)
            {
                Fact? fact = rule.GetResult();

                if (fact == null || !fact.GetVariable().GetDomain().GetValueList().Contains(fact.GetValue()))
                {
                    throw new DomainException($"Правило {rule.GetRuleName()} пытается присвоить значение не из домена!");
                }

                fact.SetRightlyType(RightlyType.Yes);
                m_expertSystem.GetProvedFacts().Add(fact);

                rule.SetWorkedType(RuleWorkType.Signifi);
                m_expertSystem.GetWorkedRules().Add(rule);
                UpdateWorkedRulesLayout();

                return RightlyType.Yes;
            }

            rule.SetWorkedType(RuleWorkType.Unsignify);
            //m_expertSystem.GetWorkedRules().Add(rule);
            UpdateWorkedRulesLayout();

            return RightlyType.Unknown;
        }
    }
}