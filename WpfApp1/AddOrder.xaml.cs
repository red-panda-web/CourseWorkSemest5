using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    public partial class AddOrder : Window
    {
        int cl_id;
        int em_id;
        int or_id;

        public AddOrder()
        {
            InitializeComponent();
            using (ADOmodel db = new ADOmodel())
            {
                or_id = db.Orders.Select(o => o.id_Order).Count() + 2;  // Создание id будущего заказа
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка id выбор клиента
        {
            try
            {
                cl_id = Convert.ToInt32(client_id.Text);

                if (cl_id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var ClientExists = db.Clients.Any(p => p.id_Client == cl_id);  // Поиск клиента с введенным id

                        if (ClientExists)   // Если такой клиент существует
                        {
                            empolyee_data.IsEnabled = true; // Активация полей
                            client_data.IsEnabled = true;
                            order_data.IsEnabled = true;
                            certificate_data.IsEnabled = true;
                            costs_data.IsEnabled = true;
                            createOrder_btn.IsEnabled = true;

                            var client = db.Clients.Where(p => p.id_Client == cl_id).FirstOrDefault(); // Выбор клиента

                            client_fullName.Text = client.Surname.ToString() + " " + client.Name.ToString() + " " + client.Patronymic.ToString();   // Заполнение ФИО клиента
                            client_phone.Content = "+" + client.Phone.ToString();   // Телефона клиента
                            client_discount.Content = db.Loyality_card.Where(lc => lc.id_Loyality_card == client.id_Loyality_card).Select(p => p.Loyality_discount).FirstOrDefault().ToString() + "%";  // Скидки по карте лояльности

                            for (int i = 1; i <= db.Position_list.Count(); i++) // Заполнение выподающего списка должности продавца
                            {
                                empl_position.Items.Add(db.Position_list.Where(pl => pl.id_Position == i).Select(pl => pl.Position_name).FirstOrDefault().ToString());
                            }
                        }
                        else MessageBox.Show("Клиент с таким id не найден!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void createOrder_btn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void empl_position_SelectionChanged(object sender, SelectionChangedEventArgs e) // Событие изменения выбраной позиции в списке "Должность"
        {
            using (ADOmodel db = new ADOmodel())
            {
                if (empl_position.SelectedItem != null)   // Если в первом выпадающем списке выбрана должность
                {
                    empl_fullName.Items.Clear(); // Список очищается от возможных предыдущих значений
                    var position_id = db.Position_list.Where(pl => pl.Position_name == empl_position.SelectedItem.ToString()).Select(pl => pl.id_Position).FirstOrDefault();   // То определяется её id
                    var employees = db.Employees.Where(em => em.id_Position == position_id).ToArray();  // И список сотрудников с данной должностью

                    for (int i = 0; i < employees.Count(); i++)    // Второй выпадающий список заполняется ФИО сотрудников выбранной должности
                    {
                        empl_fullName.Items.Add(employees[i].Surname.ToString() + " " + employees[i].Name.ToString() + " " + employees[i].Patronymic.ToString());
                    }
                }
            }
        }

        private void empl_fullName_SelectionChanged(object sender, SelectionChangedEventArgs e) // Событие изменения выбранного ФИО в списке "ФИО сотрудника"
        {
            using (ADOmodel db = new ADOmodel())
            {
                if (empl_fullName.SelectedItem != null) // Если в втором выпадающем списке выбрано ФИО 
                {
                    string[] em_fullName = empl_fullName.SelectedItem.ToString().Split(new char[] { ' ' }); // То считываем и разбиваем его на составные части
                    string em_surname = em_fullName[0]; // Составные части записываем в отдельные переменные
                    string em_name = em_fullName[1];
                    string em_patr = em_fullName[2];
                    em_id = db.Employees.Where(em => em.Surname.Equals(em_surname) && em.Name.Equals(em_name) && em.Patronymic.Equals(em_patr)).Select(em => em.id_Employee).FirstOrDefault();  // Ищем id сотрудника с таким ФИО
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   // Кнопка поиска товара
        {
            try
            {
                string name = item_search.Text;

                if (name != "")
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        bool ItemExists = db.Items.Any(i => i.Name.Contains(name)); // Проверка на существование товара

                        if (ItemExists) // Если товар существует
                        {
                            CreateOrderTable.ItemsSource = db.Items.Where(i => i.Name.Contains(name)).Join(db.Item_type, i => i.id_Type, it => it.id_Type, (i, it) => new // то он выводится в таблицу
                            {
                                i.id_Item,
                                i.Name,
                                i.Untits,
                                i.Guarantee,
                                i.Price
                            }).ToList();
                        }
                        else MessageBox.Show("Товар не найден!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Введите название товара!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        List<Item_list> new_order_list = new List<Item_list>() { }; // Список для товаров
 
        private void Button_Click_2(object sender, RoutedEventArgs e)   // Кнопка "Добавить товар"
        {
            try
            {
                int i_id = Convert.ToInt32(item_id.Text);   // Считываем id товара и его количество
                int i_count = Convert.ToInt32(item_count.Text);

                if(i_id > 0 && i_count > 0) // Если поля заполнены корректно
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        Item_list new_item = new Item_list() { id_Order = or_id, id_Item = i_id, Quantity = i_count};   // Создаем новый товар
                        new_order_list.Add(new_item);   // И заносим его в список
                    }    
                }
                else MessageBox.Show("Корректно заполните поля 'id Товара' и 'Кол-во'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните поля 'id Товара' и 'Кол-во'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)   // Кнопка "Состав заказа"
        {
            OrderList ol = new OrderList();
            ol.Owner = this;
            ol.Show();
        }
    }
}
