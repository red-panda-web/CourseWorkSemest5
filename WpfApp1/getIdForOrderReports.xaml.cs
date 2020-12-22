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
    public partial class getIdForOrderReports : Window
    {
        public getIdForOrderReports()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(orderId.Text);
                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var OrderExists = db.Orders.Any(i => i.id_Order == id); // Проверка существования заказа с указанным id
                        if (OrderExists)    // Если такой существует
                        {
                            var Order = db.Orders.Where(i => i.id_Order == id).FirstOrDefault();    // Считываем данные заказа
                            if(Order.id_Delivery != null)   // Проверяем наличие доставки у заказа
                            {

                            }
                            else
                            {

                            }
                        }    
                        else MessageBox.Show("Заказ с таким id не найден!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
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
