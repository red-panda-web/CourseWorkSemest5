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
    public partial class OrderList : Window
    {        
        public OrderList()
        {
            InitializeComponent(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка "Удалить товар"
        {
            AddOrder main = this.Owner as AddOrder;
            try
            {
                int i_id = Convert.ToInt32(item_id.Text);   // Считываем id товара
                if (i_id > 0)
                {
                    bool ItemInListExists = main.list_for_print.Exists(lfp => lfp.id == i_id);  // Проверяем его наличие в списке
                    if (ItemInListExists)
                    {
                        var pl_id = main.list_for_print.Find(lfp => lfp.id == i_id); // Находим его в списке на вывод

                        int pl_price = pl_id.CostTotal;  // Получаем цену удаляемого товара
                        main.total_cost -= pl_price;    // Пересчитываем цену заказа
                        main.costWithDiscount = main.total_cost - (main.total_cost / 100 * main.discount);
                        main.order_cost.Content = main.total_cost + " руб.";    // Выводим новую стоимость
                        main.order_discountCost.Content = main.costWithDiscount + " руб.";
                        main.list_for_print.Remove(pl_id);   // И удаляем

                        var dbl_id = main.new_order_list.Find(nol => nol.id_Item == i_id);  // Так же удаляем товар из списка для ДБ
                        main.new_order_list.Remove(dbl_id);

                        MessageBox.Show("Товар из заказа удален!\nОбновите список.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else MessageBox.Show("Данного товара нет в списке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Корректно заполните поле 'id Товара'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните поле 'id Товара'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   // Кнопка "Обновить"
        {
            AddOrder main = this.Owner as AddOrder;
            OrderListTable.ItemsSource = null;
            OrderListTable.ItemsSource = main.list_for_print;    
        }
    }
}
