using ExpertSystemCourseWork.domain;
using System;
using System.Collections;
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
