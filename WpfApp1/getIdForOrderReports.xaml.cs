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
        public static int id_order;
        public static int id_client;
        string conString;
        public getIdForOrderReports(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id_order = Convert.ToInt32(orderId.Text);
                if (id_order > 0)
                {
                    using (ADOmodel db = new ADOmodel(conString))
                    {
                        var OrderExists = db.Orders.Any(i => i.id_Order == id_order); // Проверка существования заказа с указанным id
                        if (OrderExists)    // Если такой существует
                        {
                            var Order = db.Orders.Where(i => i.id_Order == id_order).FirstOrDefault();    // Считываем данные заказа
                            id_client = Order.id_Client;

                            if(Order.id_Delivery != null)   // Проверяем наличие доставки у заказа и в зависимости от наличия вызываем нужный отчёт
                            {
                                Reports.ReportOrderWithDelivery rowd = new Reports.ReportOrderWithDelivery();
                                rowd.Show();
                            }
                            else
                            {
                                Reports.ReportOrderWithoutDelivery rowd = new Reports.ReportOrderWithoutDelivery();
                                rowd.Show();
                            }
                            this.Close();
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
