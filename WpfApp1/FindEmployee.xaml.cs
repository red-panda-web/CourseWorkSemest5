using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class FindEmployee : Window
    {
        public FindEmployee()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminPage main = this.Owner as AdminPage;
            string name = empl_Name.Text;
            string surname = empl_Surname.Text;
            string patronymic = empl_Patronymic.Text;

            if(name != "" && surname != "")
            {
                using (ADOmodel db = new ADOmodel())
                {
                    bool EmployeeExists = false;
                    if (patronymic != "") EmployeeExists = db.Employees.Any(p => p.Surname.Contains(surname) && p.Name.Contains(name) && p.Patronymic.Contains(patronymic));
                    else EmployeeExists = db.Employees.Any(p => p.Surname.Contains(surname) && p.Name.Contains(name));

                    if (EmployeeExists)
                    {
                        main.EmployeeTable.ItemsSource = (from em in db.Employees
                                                          join p in db.Position_list on em.id_Position equals p.id_Position
                                                          join r in db.Role_list on em.id_Role equals r.id_Role
                                                          where em.Surname == surname && em.Name == name
                                                          select new
                                                          {
                                                              id = em.id_Employee,
                                                              Surname = em.Surname,
                                                              Name = em.Name,
                                                              Patronymic = em.Patronymic,
                                                              Position = p.Position_name,
                                                              Role = r.Role_name,
                                                              Login = em.Login,
                                                              Password = em.Password
                                                          }).ToList();
                        this.Close();
                    }
                    else MessageBox.Show("Сотрудник не найден!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else MessageBox.Show("Заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
