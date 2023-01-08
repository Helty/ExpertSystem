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

            #region домены
            string domainName1 = "Угрозы";
            Domain domain1 = new(domainName1, new List<string>()
                {
                    "УБИ.001: Угроза автоматического распространения вредоносного кода в грид-системе",
                    "УБИ.002: Угроза агрегирования данных, передаваемых в грид-системе",
                    "УБИ.003: Угроза использования слабостей криптографических алгоритмов и уязвимостей в программном обеспечении их реализации",
                }
            );

            string domainName2 = "Да-Нет";
            Domain domain2 = new(domainName2, new List<string>()
                {
                    "Да",
                    "Нет",
                }
            );

            string domainName3 = "Источники угрозы";
            Domain domain3 = new(domainName3, new List<string>()
                {
                    "Внешний нарушитель со средним потенциалом",
                    "Внутренний нарушитель со средним потенциалом",
                }
            );

            string domainName4 = "Объекты воздействия";
            Domain domain4 = new(domainName4, new List<string>()
                {
                    "Ресурсные центры грид-системы",
                    "Сетевой трафик",
                    "Системное программное обеспечение"
                }
            );

            string domainName5 = "Последствия реализации угрозы";
            Domain domain5 = new(domainName5, new List<string>()
                {
                    "Нарушение конфиденциальности",
                    "Нарушение целостности",
                    "Нарушение доступности"
                }
            );

            string domainName6 = "Уровень автоматизации";
            Domain domain6 = new(domainName6, new List<string>()
                {
                    "Высокий",
                    "Низкий",
                }
            );

            string domainName7 = "Каналы сети интернет";
            Domain domain7 = new(domainName7, new List<string>()
                {
                    "Защищённый",
                    "Не защищённый",
                }
            );


            expertSystem.GetDomains().Add(domainName1, domain1);
            expertSystem.GetDomains().Add(domainName2, domain2);
            expertSystem.GetDomains().Add(domainName3, domain3);
            expertSystem.GetDomains().Add(domainName4, domain4);
            expertSystem.GetDomains().Add(domainName5, domain5);
            expertSystem.GetDomains().Add(domainName6, domain6);
            expertSystem.GetDomains().Add(domainName7, domain7);
            #endregion

            #region переменные
            string variableName1 = "Угроза";
            Variable variable1 = new(
                variableName1,
                domain1,
                VariableType.Deducted
            );

            string variableName2 = "Источник угрозы";
            Variable variable2 = new(
                variableName2,
                domain3,
                VariableType.Deducted
            );

            string variableName3 = "Объект воздействия";
            Variable variable3 = new(
                variableName3,
                domain4,
                VariableType.Deducted
            );

            string variableName4 = "Последствие реализации угрозы";
            Variable variable4 = new(
                variableName4,
                domain5,
                VariableType.Deducted
            );

            string variableName5 = "Возможность внедрения вредоностного кода";
            Variable variable5 = new(
                variableName5,
                domain2,
                VariableType.Queried
            );
            variable5.SetQuestion("Есть возможность внедрения вредоностного кода?");

            string variableName6 = "Уровень автоматизации";
            Variable variable6 = new(
                variableName6,
                domain6,
                VariableType.Queried
            );
            variable6.SetQuestion("Какой уровень автоматизации?");

            string variableName7 = "Канал сети Интернет";
            Variable variable7 = new(
                variableName7,
                domain7,
                VariableType.Queried
            );
            variable7.SetQuestion("Какой канал сети?");

            string variableName8 = "Возможность перехвата";
            Variable variable8 = new(
                variableName8,
                domain2,
                VariableType.Queried
            );
            variable8.SetQuestion("Есть возможность перехвата?");

            string variableName9 = "Сведения о применяемых алгоритмах";
            Variable variable9 = new(
                variableName9,
                domain2,
                VariableType.Queried
            );
            variable9.SetQuestion("Есть открытая информация о применяемых алгоритмах шифрования?");

            string variableName10 = "Новый алгоритм шифрования";
            Variable variable10 = new(
                variableName10,
                domain2,
                VariableType.Queried
            );
            variable10.SetQuestion("Используется ли новый алгоритм шифрования?");

            expertSystem.GetVariables().Add(variableName1, variable1);
            expertSystem.GetVariables().Add(variableName2, variable2);
            expertSystem.GetVariables().Add(variableName3, variable3);
            expertSystem.GetVariables().Add(variableName4, variable4);
            expertSystem.GetVariables().Add(variableName5, variable5);
            expertSystem.GetVariables().Add(variableName6, variable6);
            expertSystem.GetVariables().Add(variableName7, variable7);
            expertSystem.GetVariables().Add(variableName8, variable8);
            expertSystem.GetVariables().Add(variableName9, variable9);
            expertSystem.GetVariables().Add(variableName10, variable10);
            #endregion

            #region правила
            Rule P1 = new(
                name: "П1",
                argumentation: "Угроза заключается в возможности внедрения и запуска вредоносного кода от имени доверенного процесса на любом из ресурсных центров грид-системы и его автоматического распространения на все узлы грид-системы. Данная угроза обусловлена слабостями технологии грид-вычислений – высоким уровнем автоматизации при малой администрируемости грид-системы. Реализация данной угрозы возможна при условии наличия у нарушителя привилегий легального пользователя грид-системы",
                causes: new List<Fact>()
                {
                    new Fact(variable5, "Да", RightlyType.Unknown),
                    new Fact(variable6, "Высокий", RightlyType.Unknown),
                },
                result: new Fact(variable1, "УБИ.001: Угроза автоматического распространения вредоносного кода в грид-системе", RightlyType.Unknown)
            );

            Rule P2 = new(
                name: "П2",
                argumentation: "Угроза заключается в возможности раскрытия нарушителем защищаемой информации путём выявления задействованных в её обработке узлов, сбора, анализа и обобщения данных, перехватываемых в сети передачи данных грид-системы. Данная угроза обусловлена слабостью технологии грид-вычислений – использованием незащищённых каналов сети Интернет как транспортной сети грид-системы.Реализация данной угрозы возможна при условии наличия у нарушителя: сил и средств, достаточных для компенсации чрезвычайной распределённости грид-заданий между узлами грид-системы; привилегий, достаточных для перехвата трафика сети передачи данных между элементами (узлами) грид-системы",
                causes: new List<Fact>()
                {
                    new Fact(variable8, "Да", RightlyType.Unknown),
                    new Fact(variable7, "Не защищённый", RightlyType.Unknown),
                },
                result: new Fact(variable1, "УБИ.002: Угроза агрегирования данных, передаваемых в грид-системе", RightlyType.Unknown)
            );

            Rule P3 = new(
                name: "П3",
                argumentation: "Угроза заключается в возможности выявления слабых мест в криптографических алгоритмах или уязвимостей в реализующем их программном обеспечении. Данная угроза обусловлена слабостями криптографических алгоритмов, а также ошибками в программном коде криптографических средств, их сопряжении с системой или параметрах их настройки. Реализация угрозы возможна в случае наличия у нарушителя сведений об применяемых в системе средствах шифрования, реализованных в них алгоритмах шифрования и параметрах их настройки",
                causes: new List<Fact>()
                {
                    new Fact(variable10, "Нет", RightlyType.Unknown),
                    new Fact(variable9, "Да", RightlyType.Unknown),
                },
                result: new Fact(variable1, "УБИ.003: Угроза использования слабостей криптографических алгоритмов и уязвимостей в программном обеспечении их реализации", RightlyType.Unknown)
            );

            expertSystem.GetRules().Add(P1.GetRuleName(), P1);
            expertSystem.GetRules().Add(P2.GetRuleName(), P2);
            expertSystem.GetRules().Add(P3.GetRuleName(), P3);
            #endregion

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

            Variable variable = m_expertSystem.GetVariables()[variableName]!;

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

        #region Консультация, формирование прогноза
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
                        ? (m_expertSystem.GetTarget().GetName() == "Угроза") 
                            ? "Угроз не обнаружено, обязательно посмотрите прогноз по каждой из угроз!"
                            : "Не удалось найти истину :("
                        : result.ToString()
                    );

                    PrognosisTextBox.Text = string.Empty;

                    if (m_expertSystem.GetTarget().GetName() == "Угроза")
                    {
                        UpdatePrognosis();
                    }
                    else
                    {
                        PrognosisTextBox.Text = "Для прогноза необходимо установить цель - Угроза";
                    }
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
                if (!Fact.IsFactContains(causeFact, m_expertSystem.GetProvedFacts()))
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
                    Fact fact = Fact.GetFactFromFacts(causeFact, m_expertSystem.GetProvedFacts())!;
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
            m_expertSystem.GetWorkedRules().Add(rule);
            UpdateWorkedRulesLayout();

            return RightlyType.Unknown;
        }

        private void UpdatePrognosis()
        {
            string result = "";

            foreach (Rule rule in m_expertSystem.GetRules().Values.ToList().FindAll(rule => rule.GetResult()!.GetVariable().GetName() == "Угроза"))
            {
                List<Fact> facts = GetNeededFactToProve(rule);
                double countNeededFacts = facts.Count;
                double countProvedFacts = 0;

                string substr = "";
                foreach (Fact checkFact in facts)
                {
                    if (Fact.IsFactContains(checkFact, m_expertSystem.GetProvedFacts()))
                    {
                        if (m_expertSystem.GetProvedFacts().Find(provedFact => (checkFact.GetVariable() == provedFact.GetVariable()) && (checkFact.GetValue() == provedFact.GetValue()))!.GetRightlyType() == RightlyType.Yes)
                        {
                            countProvedFacts++;
                            substr += $"""
                                {checkFact.GetVariable().GetName()} - Обратите внимание!{System.Environment.NewLine}
                            """;
                        }
                        else
                        {
                            substr += $"""
                                {checkFact.GetVariable().GetName()} - ОК {System.Environment.NewLine}
                            """;
                        }
                    }
                    else
                    {
                        substr += $"""
                                {checkFact.GetVariable().GetName()} - Нет данных {System.Environment.NewLine}
                            """;
                    }
                }

                double threatPercentage = Math.Round((countProvedFacts / countNeededFacts) * 100);

                result += $"""
                   {rule.GetResult()!.GetValue().Split(':')[0]} - вероятность угрозы: {threatPercentage}%
                   {substr}
                   """;
            }

            PrognosisTextBox.Text = result;
        }

        private List<Fact> GetNeededFactToProve(Rule parseRule)
        {
            List <Fact> facts = new();
            foreach (Fact causesFact in parseRule.GetCauses())
            {
                if (causesFact.GetVariable().GetVariableType() == VariableType.Queried)
                {
                    facts.Add(causesFact);
                }
                else
                {
                    foreach (Rule rule in m_expertSystem.GetRules().Values.ToList().FindAll(r => r.GetResult()!.GetVariable().GetName() == causesFact.GetVariable().GetName()))
                    {
                        foreach(Fact f in GetNeededFactToProve(rule))
                        {
                            facts.Add(f);
                        }
                    }
                }
            }

            return facts;
        }
        #endregion
    }
}