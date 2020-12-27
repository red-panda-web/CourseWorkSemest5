using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static string conString;
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int CheckUser(string log, string pass)    // Функция проверки существования пользователя
        {
            conString = "data source = DESKTOP-EUNPR6C" + @"\" +"SQLEXPRESS; initial catalog = Hardware_Store; user id =" + log + "; password =" + pass +"; MultipleActiveResultSets = True; App = EntityFramework";    // Строка подключения
            using (ADOmodel db = new ADOmodel(conString))   // Новый контекст подключения с созданной строкой
            {
                int user_id;
                try
                {
                    user_id = db.Employees.Where(p => p.Login == log && p.Password == pass).Select(p => p.id_Employee).SingleOrDefault();   // Если логин и пароль корректны и подключение прошло, то в переменную запишется id сотрудника                   
                }
                catch (System.Data.SqlClient.SqlException)  // Иначе выбросится исключение
                {
                    return 0;   // Функция вернет 0
                }
                return user_id;  // При корректном срабатывании вернется id сотрудника
            }
        }

        public static int returnRole(int id)    // Функция возвращающая id роли пользователя
        {
            using (ADOmodel db = new ADOmodel(conString))
            {
                int id_role = db.Employees.Where(n => n.id_Employee == id).Select(n => n.id_Role).Single();
                return id_role;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = log.Text;    // Считываем логин и пароль
            string password = pass.Password;
            if(login != "" && password != "")   // Проверка на непустоту полей
            {
                int userID = CheckUser(login, password);    // Проверка на существование пользователя с таким логином и паролем и возврат его id
                if (userID > 0) // Если существует
                {
                    AdminPage adm = new AdminPage(conString, userID, returnRole(userID));
                    adm.Show();
                    this.Close();
                }
                else
                {
                    err.Content = "Неверный логин и/или пароль!";
                }
            }
            else err.Content = "Введите логин и пароль!";

        }
    }
}
