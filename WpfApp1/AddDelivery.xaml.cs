using System;
using System.Collections.Generic;
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
    public partial class AddDelivery : Window
    {
        int o_id;   // id заказа
        int devType_id; // id типа доставки
        decimal devCost;    // Стоимость доставки
        decimal commonIn;   // Доставка "Обычная-Город"
        decimal commonOut;  // Доставка "Обычная-Область"
        decimal urgently;   // Доставка "Срочная"
        public AddDelivery()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                o_id = Convert.ToInt32(order_id.Text);  // Считываем id товара
                if (o_id > 0)   // Проверяем его корректность
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var OrderExists = db.Orders.Any(i => i.id_Order == o_id);   // Проверяем существование заказа с таким id
                        if (OrderExists)    // Если такой заказ существует
                        {
                            var Order = db.Orders.Where(i => i.id_Order == o_id).FirstOrDefault(); // Считываем данные заказа
                            if(Order.id_Delivery == null)   // Проверяем наличие уже существующей доставки
                            {
                                delivery_data.IsEnabled = true; // Активируем блок с выбором доставки

                                for (int i = 1; i <= db.Delivery_type.Count(); i++) // Заполняем выпадающий список "Тип доставки"
                                {
                                    delivery_type.Items.Add(db.Delivery_type.Where(dt => dt.id_Type == i).Select(dt => dt.Type_name).FirstOrDefault().ToString());
                                }      
                            }
                            else MessageBox.Show("У данного заказа уже есть доставка!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else MessageBox.Show("Заказ с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void delivery_type_SelectionChanged(object sender, SelectionChangedEventArgs e) // Событие изменения выбора в выпадающем списке "Тип доставки"
        {
            if (delivery_type.SelectedItem != null)  // Если выбран тип доставки
            {
                devType_id = delivery_type.SelectedIndex + 1;   // Определяем его id

                if (devType_id == 1) devCost = commonIn;    // В зависимости от типа выбранной доставки устанавливаем её стоимость
                if (devType_id == 2) devCost = commonOut;
                if (devType_id == 3) devCost = urgently;
                delivery_cost.Content = devCost + " руб.";  // И выводим
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   // Кнопка "Добавить доставку к заказу"
        {
            if (delivery_type.SelectedItem != null) // Если выбран тип доставки
            {
                using (ADOmodel db = new ADOmodel())
                {
                    int id_deliv = db.Deliveries.Select(d => d.id_Delivery).Max() + 1;  // Создаем id будущей доставки

                    Delivery new_deliv = new Delivery() {id_Delivery = id_deliv, id_Type = devType_id, Cost = devCost };    // Создаем новую доставку
                    db.Deliveries.Add(new_deliv);   // Добавляем в БД
                    db.SaveChanges();   // Сохраняем изменения в БД

                    var Order = db.Orders.Where(i => i.id_Order == o_id).FirstOrDefault(); // Считываем данные заказа
                    Order.id_Delivery = id_deliv;   // И добавляем ему доставку
                    db.SaveChanges();   // Сохраняем изменения в БД

                    MessageBox.Show("Доставка добавлена!", "Добавление доставки", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            } 
            else MessageBox.Show("Выберите тип доставки!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)    // Событие полной загруки окна
        {
            Random rnd = new Random(); // Формируем стоимость различных доставок
            commonIn = rnd.Next(300, 1000);
            commonOut = rnd.Next(1000, 1500);
            urgently = rnd.Next(1500, 5000);
        }
    }
}
