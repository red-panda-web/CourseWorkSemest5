using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class AddNewCart : Window
    {
        public AddNewCart()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int disc = Convert.ToInt32(discount.Text);
                int amnt_prs = Convert.ToInt32(amount_purshases.Text);
                if(disc == 3 && amnt_prs < 50000 || disc == 5 && amnt_prs >= 50000 && amnt_prs < 150000 || disc == 10 && amnt_prs>=150000)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var NewCard = new Loyality_card() { Loyality_discount = disc, Amount_purshases = amnt_prs };
                        db.Loyality_card.Add(NewCard);
                        db.SaveChanges();

                        MessageBox.Show("Карта успешно создана!\nid новой карты: " + db.Loyality_card.Max(lc => lc.id_Loyality_card).ToString(), "Создание карты", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else MessageBox.Show("Некорректные данные!\nРазмер скидки и сумма покупок должны соответстоввать бизнес-правилам!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Корректно заполните поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
