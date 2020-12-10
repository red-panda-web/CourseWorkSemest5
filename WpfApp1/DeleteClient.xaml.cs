using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class DeleteClient : Window
    {
        string mode;
        public DeleteClient(string s)
        {
            InitializeComponent();
            mode = s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(id_delete.Text);
                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        if (mode == "client")
                        {
                            var ClientExists = db.Clients.Any(p => p.id_Client == id);

                            if (ClientExists)
                            {
                                var Client = db.Clients.Where(p => p.id_Client == id).FirstOrDefault();
                                var Client_adr_id = db.Client_address.Where(p => p.id_Address == db.Clients.Where(c => c.id_Client == id).Select(c => c.id_Address).FirstOrDefault()).FirstOrDefault();
                                var Client_lc = db.Loyality_card.Where(lc => lc.id_Loyality_card == db.Clients.Where(p => p.id_Client == id).Select(p => p.id_Loyality_card).FirstOrDefault()).FirstOrDefault();

                                db.Clients.Remove(Client);
                                db.Client_address.Remove(Client_adr_id);
                                if (Client_lc != null) db.Loyality_card.Remove(Client_lc);
                                db.SaveChanges();

                                MessageBox.Show("Клиент удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else MessageBox.Show("Клиент с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if(mode == "employee")
                        {
                            var EmployeeExists = db.Employees.Any(em => em.id_Employee == id);

                            if (EmployeeExists)
                            {
                                var Employee = db.Employees.Where(em => em.id_Employee == id).FirstOrDefault();
                                db.Employees.Remove(Employee);
                                db.SaveChanges();

                                MessageBox.Show("Сотрудник удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else MessageBox.Show("Сотрудник с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }                        
                    }
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
        }
    }
}
