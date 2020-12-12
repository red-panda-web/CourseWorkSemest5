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
        int id;
        public AddOrder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(client_id.Text);

                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var ClientExists = db.Clients.Any(p => p.id_Client == id);

                        if (ClientExists)
                        {
                            empolyee_data.IsEnabled = true;
                            client_data.IsEnabled = true;
                            order_data.IsEnabled = true;
                            certificate_data.IsEnabled = true;
                            costs_data.IsEnabled = true;
                            createOrder_btn.IsEnabled = true;

                            var client = db.Clients.Where(p => p.id_Client == id).FirstOrDefault();

                            client_fullName.Text = client.Surname.ToString() + " " + client.Name.ToString() + " " + client.Patronymic.ToString();
                            client_phone.Content = "+" + client.Phone.ToString();
                            client_discount.Content = db.Loyality_card.Where(lc => lc.id_Loyality_card == client.id_Loyality_card).Select(p => p.Loyality_discount).FirstOrDefault().ToString() + "%";
                            CreateOrderTable.ItemsSource = db.Items.Join(db.Item_type, i => i.id_Type, it => it.id_Type, (i, it) => new
                            {
                                i.id_Item,
                                it.Type_name,
                                i.Name,
                                i.Untits,
                                i.Guarantee,
                                i.Price
                            }).ToList();  
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

        private void CreateOrderTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*int selectedColumn = CreateOrderTable.CurrentCell.Column.DisplayIndex;
            var selectedCell = CreateOrderTable.SelectedCells[selectedColumn];
            var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            if (cellContent is TextBlock)
            {
                MessageBox.Show((cellContent as TextBlock).Text);
            }*/         
        }
    }
}
