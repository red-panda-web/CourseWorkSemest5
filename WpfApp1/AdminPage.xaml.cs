using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
            using (ADOmodel db = new ADOmodel())
            {
                all_clients.Content = db.Clients.Count().ToString();
                allEmpl.Content = db.Employees.Count().ToString();
                all_Items.Content = db.Items.Count().ToString();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)   // Пункт меню "Выход из системы"
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();     
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) // Пункт меню "Выход"
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка "Все клиенты"
        {
            using (ADOmodel db = new ADOmodel())
            {
                all_clients.Content = db.Clients.ToList().Count().ToString();
                ClientsTable.ItemsSource = (from c in db.Clients
                            join ca in db.Client_address on c.id_Address equals ca.id_Address
                            join lc in db.Loyality_card on c.id_Loyality_card equals lc.id_Loyality_card into grouping
                            from gr in grouping.DefaultIfEmpty()
                            select new
                            {
                                id = c.id_Client,
                                Surname = c.Surname,
                                Name = c.Name,
                                Patronymic = c.Patronymic,
                                Phone = c.Phone,
                                Country = ca.Country,
                                City = ca.City,
                                Street = ca.Street,
                                Building = ca.Building,
                                Flat = ca.Flat,
                                LoyalityDiscount = gr == null ? 0 : gr.Loyality_discount,
                                AmountPurshase = gr == null ? 0 : gr.Amount_purshases
                            }).ToList();
            } 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   // Кнопка "Найти клиента"
        {
            FindPerson fp = new FindPerson();
            fp.Owner = this;
            fp.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Кнопка "Добавить клиента"
        {
            AddClient ac = new AddClient();
            ac.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)   // Кнопка "Удалить клиента"
        {
            DeleteClient dc = new DeleteClient("client");
            dc.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)   // Кнопка "Изменить клиента"
        {
            ChangeClient cc = new ChangeClient();
            cc.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)   //Кнопка "Добавить новую карту лояльности"
        {
            AddNewCart anc = new AddNewCart();
            anc.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)   // Кнопка "Все сотрудники"
        {
            using (ADOmodel db = new ADOmodel())
            {
                allEmpl.Content = db.Employees.Count().ToString();
                EmployeeTable.ItemsSource = (from em in db.Employees
                                             join p in db.Position_list on em.id_Position equals p.id_Position
                                             join r in db.Role_list on em.id_Role equals r.id_Role
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
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)   // Кнопка "Добавить сотрудника"
        {
            AddEmployee ae = new AddEmployee();
            ae.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)   // Кнопка "Изменить сотрудника"
        {
            ChangeEmployee ce = new ChangeEmployee();
            ce.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)   // Кнопка "Удалить сотрудника"
        {
            DeleteClient dc = new DeleteClient("employee");
            dc.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)  // Кнопка "Найти сотрудника"
        {
            FindEmployee fe = new FindEmployee();
            fe.Owner = this;
            fe.Show();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)  // Кнопка "Список товаров"
        {
            using (ADOmodel db = new ADOmodel())
            {
                all_Items.Content = db.Items.Count().ToString();
                ItemsTable.ItemsSource = db.Items.Join(db.Item_type, i => i.id_Type, it => it.id_Type, (i, it) => new
                {
                    i.id_Item,
                    it.Type_name,
                    i.Name,
                    i.Untits,
                    i.Photo,
                    i.Guarantee,
                    i.Price
                }).ToList();
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)  // Кнопка "Найти товар"
        {
            FindItem fi = new FindItem();
            fi.Owner = this;
            fi.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)  // Кнопка "Добавить товар"
        {
            AddItem ai = new AddItem();
            ai.Show();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)  // Кнопка "Изменить товар"
        {
            ChangeItem ci = new ChangeItem();
            ci.Show();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)  // Кнопка "Удалить товар"
        {
            DeleteClient di = new DeleteClient("item");
            di.Show();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)  // Кнопка "Список заказов"
        {
            using (ADOmodel db = new ADOmodel())
            {
                OrdersTable.ItemsSource = (from o in db.Orders 
                                           join em in db.Employees on o.id_Employee equals em.id_Employee
                                           join cl in db.Clients on o.id_Client equals cl.id_Client
                                           join c in db.Certificates on o.id_Certificate equals c.id_Certificate into grouping0
                                           from gr0 in grouping0.DefaultIfEmpty()
                                           join lc in db.Loyality_card on cl.id_Loyality_card equals lc.id_Loyality_card into grouping1
                                           from gr1 in grouping1.DefaultIfEmpty()
                                           join d in db.Deliveries on o.id_Delivery equals d.id_Delivery into grouping2
                                           from gr2 in grouping2.DefaultIfEmpty()
                                           join dt in db.Delivery_type on gr2.id_Type equals dt.id_Type into grouping3
                                           from gr3 in grouping3.DefaultIfEmpty()
                                           select new
                                           {
                                               id = o.id_Order,
                                               employee = em.Surname + " " + em.Name + " " + em.Patronymic,
                                               client = cl.Surname + " " + cl.Name + " " + cl.Patronymic,
                                               date = o.Date,
                                               order_cost = o.Order_cost,
                                               loyality_discount = gr1 == null ? 0 : gr1.Loyality_discount,
                                               certificate = gr0 == null ? 0 : gr0.Value, 
                                               delivery = gr2 == null ? "-" : gr3.Type_name,                               
                                           }).ToList();
            }
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)  // Кнопка "Добавить заказ"
        {
            AddOrder ao = new AddOrder();
            ao.Show();
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)  // Кнопка "Удалить заказ"
        {
            DeleteClient dc = new DeleteClient("order");
            dc.Show();
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)  // Кнопка "Список доставок"
        {
            using (ADOmodel db = new ADOmodel())
            {
                OrdersTable.ItemsSource = db.Deliveries.Join(db.Delivery_type, d => d.id_Type, dt => dt.id_Type, (d, dt) => new
                {
                    d.id_Delivery,
                    dt.Type_name,
                    d.Cost
                }).Join(db.Orders, d => d.id_Delivery, o => o.id_Delivery, (d, o) => new
                {
                    d.id_Delivery,
                    o.id_Order,
                    d.Type_name,
                    d.Cost
                }).ToList();
            }
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)  // Кнопка "Добавить доставку"
        {
            AddDelivery ad = new AddDelivery();
            ad.Show();
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)  // Кнопка "Удалить доставку"
        {
            DeleteClient dc = new DeleteClient("delivery");
            dc.Show();
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)  // Отчёт "ТОП-10 товаров"
        {
            Reports.ReportTop10 rt10 = new Reports.ReportTop10();
            rt10.Show();
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)  // Отчёт "Остатки товара"
        {
            Reports.ReportRemains rr = new Reports.ReportRemains();
            rr.Show();
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)  // Отчёт "Отпуск товара"
        {
            Reports.ReportItemsRelease rir = new Reports.ReportItemsRelease();
            rir.Show();
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)  // Отчёт "Приход товара"
        {
            Reports.ReportItemsComing ric = new Reports.ReportItemsComing();
            ric.Show();
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)  // Отчёт "Состав заказа"
        {
            getIdForOrderReports gid = new getIdForOrderReports();
            gid.Show();
        }
    }
}
