using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class AddOrder : Window
    {
        int cl_id;  // id клиента
        int em_id;  // id сотрудника
        int or_id;  // id заказа
        int devType_id; // id типа доставки
        int devCost = 0;    // Стоимость доставки
        int commonIn;   // Доставка "Обычная-Город"
        int commonOut;  // Доставка "Обычная-Область"
        int urgently;   // Доставка "Срочная"
        Client client;  // Сущность клиента
        public List<Item_list> new_order_list = new List<Item_list>() { }; // Список товаров для БД
        public List<PrintList> list_for_print = new List<PrintList>() { };  // Список товаров для вывода
        public double total_cost = 0;  // Общая сумма заказа
        public double costWithDiscount = 0;    // Общая сумма заказа с скидкой по карте лояльности
        public int discount = 0;    // % скидки по карте лояльности

        public AddOrder()
        {
            InitializeComponent();
            using (ADOmodel db = new ADOmodel())
            {
                or_id = db.Orders.Select(o => o.id_Order).Max() + 1;  // Создание id будущего заказа
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
                            delivery_cb.IsEnabled = true;
                            client_block.IsEnabled = false; // Деактивация блока ввода id клиента
                            order_date.Content = DateTime.Now.ToString();   // Вывод даты формирования заказа

                            client = db.Clients.Where(p => p.id_Client == cl_id).FirstOrDefault(); // Выбор клиента

                            if(client.Patronymic != null) client_fullName.Text = client.Surname.ToString() + " " + client.Name.ToString() + " " + client.Patronymic.ToString();   // Заполнение ФИО клиента
                            else client_fullName.Text = client.Surname.ToString() + " " + client.Name.ToString();  

                            client_phone.Content = "+" + client.Phone.ToString();   // Телефона клиента
                            client_discount.Content = db.Loyality_card.Where(lc => lc.id_Loyality_card == client.id_Loyality_card).Select(p => p.Loyality_discount).FirstOrDefault().ToString() + "%";  // Скидка по карте лояльности

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

        private void empl_position_SelectionChanged(object sender, SelectionChangedEventArgs e) // Событие изменения выбраной позиции в списке "Должность"
        {
            using (ADOmodel db = new ADOmodel())
            {
                if (empl_position.SelectedItem != null)   // Если в первом выпадающем списке выбрана должность
                {
                    empl_fullName.Items.Clear(); // Список очищается от возможных предыдущих значений
                    var position_id = db.Position_list.Where(pl => pl.Position_name == empl_position.SelectedItem.ToString()).Select(pl => pl.id_Position).FirstOrDefault();   // Определяется id должности
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
                    em_id = db.Employees.Where(em => em.Surname.Equals(em_surname) && em.Name.Equals(em_name) && em.Patronymic.Equals(em_patr)).Select(em => em.id_Employee).FirstOrDefault();  // Определяем id сотрудника с таким ФИО
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

        private void Button_Click_2(object sender, RoutedEventArgs e)   // Кнопка "Добавить товар"
        {
            try
            {
                int i_id = Convert.ToInt32(item_id.Text);   // Считываем id товара и его количество
                int i_count = Convert.ToInt32(item_count.Text);

                if (i_id > 0 && i_count > 0) // Если поля заполнены корректно
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        bool ItemExist = db.Items.Any(i => i.id_Item == i_id);  // Если товар существует
                        if (ItemExist)
                        {
                            discount = db.Loyality_card.Where(lc => lc.id_Loyality_card == client.id_Loyality_card).Select(lc => lc.Loyality_discount).FirstOrDefault(); // Считываем скидку

                            Item_list new_item = new Item_list() { id_Order = or_id, id_Item = i_id, Quantity = i_count };   // Создаем новый товар
                            new_order_list.Add(new_item);   // И заносим его в список для БД

                            string i_name = db.Items.Where(i => i.id_Item == i_id).Select(i => i.Name).FirstOrDefault().ToString();  // Запоминаем имя товара
                            int i_price = (int)db.Items.Where(i => i.id_Item == i_id).Select(i => i.Price).FirstOrDefault();    // И его стоимость
                            PrintList pr_item = new PrintList() { id = i_id, ItemName = i_name, Quantity = i_count, CostTotal = i_price * i_count, CostOne = i_price }; // Заносим данные в список для вывода
                            list_for_print.Add(pr_item);

                            total_cost += pr_item.CostTotal; // Расчет стоимости
                            costWithDiscount = total_cost - (total_cost / 100 * discount);

                            order_cost.Content = total_cost + " руб.";    // Вывод стоимости
                            order_discountCost.Content = costWithDiscount + " руб.";
                        }
                        else MessageBox.Show("Товар не найден!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public class PrintList  // Класс для списка товаров на вывод
        {
            public int id { get; set; }
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public int CostOne { get; set; }
            public int CostTotal { get; set; }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)   // Кнопка "Применить сертификат"
        {
            try
            {
                int certificateValue = Convert.ToInt32(item_certificate.Text);  // Считываем номинал сертификата
                if(certificateValue == 5000 || certificateValue == 30000 || certificateValue == 50000)  // Если номинал соответсятвует бизнес-правилам
                {
                    MessageBoxResult confirm = MessageBox.Show("Сертификат должен применяться в самом конце оформления заказа, после полностью сформированного списка товаров и выбранной доставки!\n" +
                        "При изменении данных заказа или доставки ПОСЛЕ применения сертификата возможен неккоректный расчет окончательной цены!\nПрименить сертификат?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning); // Предупреждение
                    if(confirm == MessageBoxResult.Yes) // Если нажата кнопка "Да"
                    {
                        total_cost -= certificateValue; // Уменьшаем текущую общую стоимость на номинал сертификата
                        if (total_cost < 0) total_cost = 1; // Если номинал сертификата превышает сумму покупки, то сумма покупки становиться равна 1руб.
                        costWithDiscount = total_cost - (total_cost / 100 * discount);  // Пересчитываем стоимость с учетом скидки по КЛ

                        order_cost.Content = total_cost + " руб.";   // Выводим новую стоимость
                        order_discountCost.Content = costWithDiscount + " руб.";

                        certificate_data.IsEnabled = false; // Деактивируем блок "Сертификат"
                    }
                }else if (certificateValue != 0) MessageBox.Show("Корректно заполните поле 'Номинал'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните поле 'Номинал'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void delivery_cb_Click(object sender, RoutedEventArgs e)    // Чекбокс "Доставка"
        {
            if(delivery_cb.IsChecked == true)   // Если чекбокс выбран
            {
                delivery_data.IsEnabled = true; // Активируем блок доставки

                using (ADOmodel db = new ADOmodel())
                {
                    var client_adr = db.Client_address.Where(ca => ca.id_Address == client.id_Address).FirstOrDefault();    // Находим адрес клиента

                    address_country.Content = client_adr.Country;   // И заполняем поля
                    address_city.Content = client_adr.City;
                    address_street.Content = client_adr.Street;
                    address_building.Content = client_adr.Building;
                    if (client_adr.Flat != null) address_flat.Content = client_adr.Flat;

                    for (int i = 1; i <= db.Delivery_type.Count(); i++) // Заполняем выпадающий список "Тип доставки"
                    {
                        delivery_type.Items.Add(db.Delivery_type.Where(dt => dt.id_Type == i).Select(dt => dt.Type_name).FirstOrDefault().ToString());
                    }
                }                 
            }
            else delivery_data.IsEnabled = false;   // Иначе деактивируем блок
        }

        private void delivery_type_SelectionChanged(object sender, SelectionChangedEventArgs e) // Событие изменения выбора в выпадающем списке "Тип доставки"
        {
            if(delivery_type.SelectedItem != null)  // Если выбран тип доставки
            {
                devType_id = delivery_type.SelectedIndex + 1;   // Определяем его id

                if (devType_id == 1) devCost = commonIn;    // В зависимости от типа выбранной доставки устанавливаем её стоимость
                if (devType_id == 2) devCost = commonOut;
                if (devType_id == 3) devCost = urgently;
                delivery_cost.Content = devCost + " руб.";  // И выводим
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)   // Кнопка "Принять тип доставки"
        {
            if (delivery_type.SelectedItem != null) // Если выбран тип доставки
            {
                total_cost += devCost;  // Добавляем стоисоть доставки к общей сумме заказа
                costWithDiscount = total_cost - (total_cost / 100 * discount);  // Пересчитываем стоимость с учетом скидки по КЛ

                order_cost.Content = total_cost + " руб.";   // Выводим новую стоимость
                order_discountCost.Content = costWithDiscount + " руб.";

                delivery_type.IsEnabled = false;    // Деактивируем список выбор и кнопку
                delivery_select_btn.IsEnabled = false;
            }
            else MessageBox.Show("Выберите тип доставки!", "Подтверждение доставки", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void createOrder_btn_Click(object sender, RoutedEventArgs e)    // Кнопка оформления заказа
        {
            bool canSend = true;    // Переменная для контроля корректности заполнения данных

            if (empl_position.SelectedItem == null || empl_fullName.SelectedItem == null)   // Проверки на правильную заполненность полей
            {
                MessageBox.Show("Выберите сотрудника!", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                canSend = false;
            }
            if (new_order_list.Count() == 0 || list_for_print.Count() == 0) {
                MessageBox.Show("Нельзя оформить пустой заказ!", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                canSend = false;
            }
            if (new_order_list.Count() != list_for_print.Count()) {
                MessageBox.Show("Ошибка в формировании списков заказа!", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                canSend = false;
            }
            if (item_certificate.Text == "") {
                MessageBox.Show("Поле 'Номинал' блока 'Сертификат' должно содержать значение!\n(0, 5000, 30000, 50000)", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                canSend = false;
            }
            if (delivery_cb.IsChecked == true && delivery_type.IsEnabled == true && delivery_select_btn.IsEnabled == true) {
                MessageBox.Show("Подтвердите тип доставки!", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Warning);
                canSend = false;
            } 

            if(canSend){    // Если все заполнено корректно, то начинаем отправлять данные в БД
                using (ADOmodel db = new ADOmodel())
                {
                    int id_cert = 0;    // Идентификатор нового сертификата
                    int id_deliv = 0;   // Идентификатор новой доставки

                    if(Convert.ToInt32(item_certificate.Text) != 0) // Если был применен сертификат, то создаем запись о сертификате
                    {
                        id_cert = db.Certificates.Select(c => c.id_Certificate).Max() + 1; 
                        int certificateValue = Convert.ToInt32(item_certificate.Text);
                        Certificate new_certificate = new Certificate() {id_Certificate = id_cert, Value = certificateValue };
                        db.Certificates.Add(new_certificate);
                        db.SaveChanges();
                    }

                    if(delivery_cb.IsChecked == true)   // Если была выбрана доставка, то создаем запись по доставке
                    {
                        id_deliv = db.Deliveries.Select(d => d.id_Delivery).Max() + 1;
                        Delivery new_delivery = new Delivery() {id_Delivery = id_deliv, id_Type = devType_id, Cost = devCost };
                        db.Deliveries.Add(new_delivery);
                        db.SaveChanges();
                    }

                    if(delivery_cb.IsChecked == false && devCost > 0 && delivery_select_btn.IsEnabled == false)   // Если доставка была сначала выбрана и применена, но потом чекбокс доставки был снят
                    {
                        total_cost -= devCost;  // Убираем из стоимости заказа доставку
                        costWithDiscount = total_cost - (total_cost / 100 * discount);  // И пересчитываем стоимость заказа с учетом КЛ
                    }

                    Orders new_order;

                    if (id_cert != 0 && id_deliv != 0)  // Формирование заказа с сертификатом и доставкой
                    {
                        new_order = new Orders()
                        {
                            id_Order = or_id,
                            id_Client = cl_id,
                            id_Employee = em_id,
                            Date = DateTime.Now,
                            Order_cost = (decimal)costWithDiscount,
                            Discount = discount,
                            id_Certificate = id_cert,
                            id_Delivery = id_deliv
                        };
                        db.Orders.Add(new_order);
                    }
                    else if (id_cert == 0 && id_deliv != 0) // Формирование заказа без сертификата, но с доставкой
                    {
                        new_order = new Orders()
                        {
                            id_Order = or_id,
                            id_Client = cl_id,
                            id_Employee = em_id,
                            Date = DateTime.Now,
                            Order_cost = (decimal)costWithDiscount,
                            Discount = discount,
                            id_Delivery = id_deliv
                        };
                        db.Orders.Add(new_order);
                    }
                    else if(id_cert != 0 && id_deliv == 0)  // Формирование заказа с сертификатом, но без доставки
                    {
                        new_order = new Orders()
                        {
                            id_Order = or_id,
                            id_Client = cl_id,
                            id_Employee = em_id,
                            Date = DateTime.Now,
                            Order_cost = (decimal)costWithDiscount,
                            Discount = discount,
                            id_Certificate = id_cert,
                        };
                        db.Orders.Add(new_order);
                    }
                    else // Формирование заказа без сертификата и без доставки
                    {
                        new_order = new Orders()
                        {
                            id_Order = or_id,
                            id_Client = cl_id,
                            id_Employee = em_id,
                            Date = DateTime.Now,
                            Order_cost = (decimal)costWithDiscount,
                            Discount = discount,
                        };
                        db.Orders.Add(new_order);
                    }

                    foreach(var item in new_order_list) // Добавление товаров заказа в таблицу Item_list
                    {
                        int il_id = item.id_Item;
                        int il_quant = item.Quantity;
                        Item_list il = new Item_list()
                        {
                            id_Order = or_id,
                            id_Item = il_id,
                            Quantity = il_quant
                        };
                        db.Item_list.Add(il);
                    }

                    db.SaveChanges();
                    MessageBox.Show("Заказ успешно добавлен!", "Оформление заказа", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random(); // Формируем стоимость различных доставок
            commonIn = rnd.Next(300, 1000);
            commonOut = rnd.Next(1000, 1500);
            urgently = rnd.Next(1500, 5000);
        }
    }
}
