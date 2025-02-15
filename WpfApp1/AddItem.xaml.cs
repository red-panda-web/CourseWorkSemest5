﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class AddItem : Window
    {
        string conString;
        public AddItem(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = item_Name.Text;
            string units = item_units.Text;
            string url = item_photo.Text;
            string type = item_type.Text;
            try
            {
                int guarantee = Convert.ToInt32(item_guarantee.Text);
                int price = Convert.ToInt32(item_price.Text);

                if (name != "" && units != "" && url != "" && price > 0 && type != null)
                {
                    using (ADOmodel db = new ADOmodel(conString))
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
            catch (FormatException)
            {
                MessageBox.Show("Корректно заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
    }
}
