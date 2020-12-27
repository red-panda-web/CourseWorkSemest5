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
    public partial class ItemCard : Window
    {
        int id_item;
        string conString;
        public ItemCard(int i, string str)
        {
            InitializeComponent();
            id_item = i;
            conString = str;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ADOmodel db = new ADOmodel(conString))
            {
                var item = db.Items.Where(i => i.id_Item == id_item).FirstOrDefault();
                var category = db.Item_type.Where(it => it.id_Type == item.id_Type).FirstOrDefault();

                item_Name.Text = item.Name.ToString();
                item_id.Content += item.id_Item.ToString();
                try
                {
                    item_Img.Source = new BitmapImage(new Uri(item.Photo.ToString()));
                }
                catch (UriFormatException)
                {
                    MessageBox.Show("Товар имеет неккоректную ссылку на изображение.\nИзображение не будет загружено!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }  
                item_Categoty.Content = category.Type_name.ToString();
                item_Units.Content = item.Untits.ToString();
                if(item.Guarantee != null) item_Garantee.Content = item.Guarantee.ToString();
                item_Price.Content = item.Price.ToString() + "руб.";
            }
        }
    }
}
