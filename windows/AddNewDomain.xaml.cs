using ExpertSystemCourseWork;
using ExpertSystemCourseWork.domain;
using System.Linq;
using System.Windows;

namespace ExpertSystemCourseWork.windows
{
    public partial class AddNewDomain : Window
    {
        private ExpertSystem m_expertSystem;

        public ExpertSystem ReturnExpertSystem
        {
            get { return m_expertSystem; }
        }

        public AddNewDomain(ExpertSystem expertSystem)
        {
            InitializeComponent();

            m_expertSystem = expertSystem;
        }

        private void AddNewValueDomainButton_Click(object sender, RoutedEventArgs e)
        {
            string domainNameToChange = InputNewDomainNameTextBox.Text;

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
                MessageBox.Show("Домен к которому будет добавляться значение - не задан.");
            }
        }

        private void AddNewDomainButton_Click(object sender, RoutedEventArgs e)
        {
            string domainNameToChange = InputNewDomainNameTextBox.Text;

            if (domainNameToChange != string.Empty)
            {
                if (!m_expertSystem.GetDomains().Keys.Contains(domainNameToChange))
                {
                    m_expertSystem.GetDomains().Add(domainNameToChange, new Domain(domainNameToChange, new()));
                }

                MessageBox.Show($"Новый домен: '{domainNameToChange}' успешно добавлен.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Новый домен не создан");
            }
        }

        private void UpdateDomainValuesLayout(string domainName)
        {
            DomainValueComboBox.Items.Clear();
            foreach (string valueName in m_expertSystem.GetDomains()[domainName].GetValueList())
            {
                DomainValueComboBox.Items.Add(valueName);
            }
            DomainValueComboBox.SelectedIndex = 0;
        }
    }
}
