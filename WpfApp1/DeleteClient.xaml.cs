using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class DeleteClient : Window
    {
        string conString;
        string mode;
        public DeleteClient(string s, string str)
        {
            InitializeComponent();
            mode = s;
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(id_delete.Text);
                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel(conString))
                    {
                        if (mode == "client")   // Если форма удаления была вызвана из вкладки "Клиент"
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
                        else if(mode == "employee") // Если форма удаления была вызвана из вкладки "Сотрудники"
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
                        else if(mode == "item") // Если форма удаления была вызвана из вкладки "Товары"
                        {
                            var ItemExists = db.Items.Any(i => i.id_Item == id);
                            if (ItemExists)
                            {
                                var Item = db.Items.Where(i => i.id_Item == id).FirstOrDefault();
                                db.Items.Remove(Item);
                                db.SaveChanges();

                                MessageBox.Show("Товар удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else MessageBox.Show("Товар с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if (mode == "order")   // Если форма удаления была вызвана из вкладки "Заказы"
                        {
                            var OrderExists = db.Orders.Any(i => i.id_Order == id); // Проверка существования заказа с указанным id
                            if (OrderExists)    // Если такой существует
                            {
                                var Order = db.Orders.Where(i => i.id_Order == id).FirstOrDefault();    // Считываем данные заказа
                                var Delivery = db.Deliveries.Where(d => d.id_Delivery == Order.id_Delivery).FirstOrDefault();   // И доставки 

                                if (Order.id_Certificate != null)   // Проверяем был ли применен сертификат, если да, то удаляем его
                                {
                                    var cert_id = db.Certificates.Where(c => c.id_Certificate == Order.id_Certificate).FirstOrDefault();
                                    db.Certificates.Remove(cert_id);
                                }
                                db.Orders.Remove(Order);    // Удаляем заказ
                                if (Delivery != null) db.Deliveries.Remove(Delivery);   // Если была доставка, то и её тоже

                                var items = db.Item_list.Where(il => il.id_Order == Order.id_Order).ToList();   // Находим все товары из заказа
                                for(int i = 0; i< items.Count(); i++)   // И удаляем их их таблицы учета Item_list
                                {
                                    db.Item_list.Remove(items[i]);
                                }

                                db.SaveChanges();

                                MessageBox.Show("Заказ удален!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else MessageBox.Show("Заказ с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else if(mode == "delivery") // Если форма удаления была вызвана из блока "Доставки"
                        {
                            var DeliveryExists = db.Deliveries.Any(d => d.id_Delivery == id);   // Проверяем существование доставки с  указанным id
                            if (DeliveryExists) // Если такая существует
                            {
                                var Delivery = db.Deliveries.Where(d => d.id_Delivery == id).FirstOrDefault();  // выбираем её и заказ, которому она принадлежит
                                var Order = db.Orders.Where(o => o.id_Delivery == id).FirstOrDefault();

                                db.Deliveries.Remove(Delivery); // Удаляем доставку
                                Order.id_Delivery = null;   // Убираем ее из заказа
                                db.SaveChanges();   // Сохраняем изменения в БД
                                MessageBox.Show("Доставки удалена!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else MessageBox.Show("Доставки с таким id не существует!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
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
