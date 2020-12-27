using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class FindItem : Window
    {
        string conString;
        public FindItem(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdminPage main = this.Owner as AdminPage;
                string name = items_name.Text;

                if(name != "")
                {
                    using (ADOmodel db = new ADOmodel(conString))
                    {
                        bool ItemExists = db.Items.Any(i => i.Name.Contains(name)); // Проверка на существование товара

                        if (ItemExists)
                        {
                            int ItemsCount = db.Items.Where(i => i.Name.Contains(name)).Count();    // Подсчет количества товара
                            
                            if(ItemsCount > 1) {
                                main.ItemsTable.ItemsSource = db.Items.Where(i => i.Name.Contains(name)).Join(db.Item_type, i => i.id_Type, it => it.id_Type, (i, it) => new // Если товаров более 1, то результат поиска выводится в таблицу
                                {
                                    i.id_Item,
                                    it.Type_name,
                                    i.Name,
                                    i.Untits,
                                    i.Photo,
                                    i.Guarantee,
                                    i.Price
                                }).ToList();
                            }
                            else
                            {
                                int itemId = db.Items.Where(i => i.Name.Contains(name)).Select(i => i.id_Item).FirstOrDefault();    // Если товар 1, то берем его id и открываем описание в отдельном окне
                                ItemCard ic = new ItemCard(itemId, conString);
                                ic.Show();
                            }

                            this.Close();
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
    }
}
