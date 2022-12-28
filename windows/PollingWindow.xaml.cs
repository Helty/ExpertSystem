using ExpertSystemCourseWork.domain;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ExpertSystemCourseWork.windows
{

    public partial class PollingWindow : Window
    {
        string m_userResult = "";
        List<RadioButton> m_radioButtonList = new();

        public string ReturnUserAnswer
        {
            get { return m_userResult; }
        }

        public PollingWindow(Variable target)
        {
            InitializeComponent();

            ShowQuestion(target);
        }

        private void ShowQuestion(Variable target)
        {
            QuestionTextBox.Text = (target.GetQuestion() == null || target.GetQuestion()!.Trim() == "")
                ? target.GetName() + " = ?"
                : target.GetQuestion();

            StackPanel stackPanel = new();
            foreach (string domainValue in target.GetDomain().GetValueList())
            {
                RadioButton newRadioButton = new() { Content = domainValue };

                m_radioButtonList.Add(newRadioButton);
                stackPanel.Children.Add(newRadioButton);
            }

            AnswerOptionsGroupBox.Content = stackPanel;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (RadioButton radioButton in m_radioButtonList)
            {
                if (radioButton.IsChecked == true)
                {
                    m_userResult = radioButton.Content.ToString()!;
                    Close();
                }
            }
        }
    }
}
