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
    /// <summary>
    /// Логика взаимодействия для AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = item_Name.Text;
            string units = item_units.Text;
            string url = item_photo.Text;
            string type = item_type.Text;
            int guarantee = 0;
            try
            {
                if (Convert.ToInt32(item_guarantee.Text) > 0) guarantee = Convert.ToInt32(item_guarantee.Text);
                int price = Convert.ToInt32(item_price.Text);

                if(name !="" && units !="" && url != "" && price > 0 && type != null)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        int typeId = db.Item_type.Where(it => it.Type_name.Equals(type)).Select(it => it.id_Type).FirstOrDefault();

                        if (guarantee == 0)
                        {
                            Item new_item = new Item() { id_Type = typeId, Name = name, Photo = url, Untits = units, Price = price };
                            db.Items.Add(new_item);
                        }
                        else
                        {
                            Item new_item = new Item() { id_Type = typeId, Name = name, Photo = url, Untits = units, Price = price, Guarantee = guarantee };
                            db.Items.Add(new_item);
                        }

                        db.SaveChanges();
                        MessageBox.Show("Товар успешно добавлен!", "Добавление товара", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else MessageBox.Show("Корректно заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
