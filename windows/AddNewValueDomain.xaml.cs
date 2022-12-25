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
    public partial class AddNewValueDomain : Window
    {
        private ExpertSystem m_expertSystem;
        private string m_domainNameToChange;

        public ExpertSystem ReturnExpertSystem
        {
            get { return m_expertSystem; }
        }

        public AddNewValueDomain(ExpertSystem expertSystem, string domainNameToChange)
        {
            InitializeComponent();

            m_expertSystem = expertSystem;
            m_domainNameToChange = domainNameToChange;
        }

        private void AddNewValueDomainButton_Click(object sender, RoutedEventArgs e)
        {
            string newDomainValue = InputNewValueDomainTextBox.Text;

            if (m_expertSystem.GetDomains().Keys.Contains(m_domainNameToChange) && m_expertSystem.GetDomains()[m_domainNameToChange].GetValueList().Contains(newDomainValue))
            {
                MessageBox.Show($"Значение: {newDomainValue} для домена: '{m_domainNameToChange}' уже существует.");
                return;
            }


            if (newDomainValue != string.Empty)
            {
                if (m_expertSystem.GetDomains().Keys.Contains(m_domainNameToChange))
                {
                    m_expertSystem.GetDomains()[m_domainNameToChange].GetValueList().Add(newDomainValue);
                }
                else
                {
                    m_expertSystem.GetDomains().Add(m_domainNameToChange, new Domain(m_domainNameToChange, new(){ newDomainValue }));
                }

                MessageBox.Show($"Новое значение: '{newDomainValue}' для домена: '{m_domainNameToChange}' успешно добавлено.");
                this.Close();
            }
            else
            {
                MessageBox.Show($"Новое значение для домена: '{m_domainNameToChange}' не заданo.");
            }
        }
    }
}
