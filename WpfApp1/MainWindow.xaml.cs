using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int CheckUser(string log, string pass)
        {
            using (ADOmodel db = new ADOmodel())
            {
                var user = db.Employees.Where(p => p.Login == log && p.Password == pass).Select(p => p.id_Employee);
                if (user.SingleOrDefault() != null) return user.SingleOrDefault();
                else return 0;
            }
        }

        public static int returnRole(int id)
        {
            using (ADOmodel db = new ADOmodel())
            {
                var id_role = db.Employees.Where(n => n.id_Employee == id).Select(n => n.id_Role).Single();
                return id_role;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoadImg.Opacity = 100;
            string login = log.Text;
            string password = pass.Password;
            int userID = CheckUser(login, password);
            if (userID > 0)
            {
                switch (returnRole(userID))
                {
                    case 1:
                        AdminPage adm = new AdminPage();
                        adm.Show();
                        this.Close();
                        break;
                    case 2:
                        WRHManagerPage wrhmng = new WRHManagerPage();
                        wrhmng.Show();
                        this.Close();
                        break;
                    case 3:
                        ConsultantPage cnslt = new ConsultantPage();
                        cnslt.Show();
                        this.Close();
                        break;
                    case 4:  
                        SalesManagerPage slsmng = new SalesManagerPage();
                        slsmng.Show();
                        this.Close();
                        break;
                }
            }
            else
            {
                err.Content = "Неверный логин и/или пароль!";
            }

        }
    }
}
