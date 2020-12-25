using System.Windows;

namespace WpfApp1
{
    public partial class AddEmployee : Window
    {
        string conString;
        public AddEmployee(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = empl_Name.Text;
            string surname = empl_Surname.Text;
            string patronymic = empl_Patronymic.Text;
            var pos = empl_Position.SelectedIndex;
            var role = empl_Role.SelectedIndex;
            string log = empl_login.Text;
            string pas = empl_password.Text;

            if(name != "" && surname != "" && log != "" && pas != "" && pos != -1 && role != -1)
            {
                using (ADOmodel db = new ADOmodel(conString))
                {
                    if (patronymic == "")
                    {
                        var NewEmpl = new Employee() { Name = name, Surname = surname, Login = log, Password = pas, id_Position = pos + 1, id_Role = role + 1 };
                        db.Employees.Add(NewEmpl);
                    }
                    else
                    {
                        var NewEmpl = new Employee() { Name = name, Surname = surname, Patronymic = patronymic, Login = log, Password = pas, id_Position = pos + 1, id_Role = role + 1 };
                        db.Employees.Add(NewEmpl);
                    }

                    db.SaveChanges();
                    MessageBox.Show("Сотрудник успешно создан!", "Регистрация нового сотрудника", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else MessageBox.Show("Заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
