using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class ChangeItem : Window
    {
        int id;
        string conString;
        public ChangeItem(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(item_id.Text);

                if(id > 0)
                {
                    using (ADOmodel db = new ADOmodel(conString))
                    {
                        var ItemExists = db.Items.Any(i => i.id_Item == id);

                        if (ItemExists)
                        {
                            item_data.IsEnabled = true;
                            item_btn.IsEnabled = true;
                            item_found.Content = "Товар найден!";
                        }
                        else MessageBox.Show("Товар с таким id не найден!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void item_guarantee_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void item_price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void item_btn_Click(object sender, RoutedEventArgs e)
        {
            string name = item_Name.Text;
            string units = item_units.Text;
            string url = item_photo.Text;
            string type = item_type.Text;

            try
            {
                int guarantee = Convert.ToInt32(item_guarantee.Text);
                int price = Convert.ToInt32(item_price.Text);

                using (ADOmodel db = new ADOmodel(conString))
                {
                    var Item = db.Items.Where(i => i.id_Item == id).FirstOrDefault();

                    if (name != "") Item.Name = name;
                    if (units != "") Item.Untits = units;
                    if (url != "") Item.Photo = url;
                    if (type != "") Item.id_Type = db.Item_type.Where(it => it.Type_name.Equals(type)).Select(it => it.id_Type).FirstOrDefault();
                    if (guarantee > 0) Item.Guarantee = guarantee;
                    if (price > 0) Item.Price = price;

                    db.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Корректно заполните поля!\n(Поля Гарантия и Цена должны быть заполнены, либо 0 либо новым значением!)", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
