using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class AddClient : Window
    {
        string conString;
        public AddClient(string str)
        {
            InitializeComponent();
            conString = str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string surname = Surname.Text;
            string patronymic = Patronymic.Text;
            string phone = Phone.Text;
            string country = Country.Text;
            string city = City.Text;
            string street = Street.Text;
            string building = Building.Text;
            string flat = Flat.Text;

            if (name.Length != 0 && surname.Length != 0 && phone.Length == 11 && country.Length != 0 && city.Length != 0 &&street.Length != 0 && building.Length != 0)
            {
                using (ADOmodel db = new ADOmodel(conString))
                {
                    Client_address new_address = new Client_address() { Country = country, City = city, Street = street, Building = building, Flat = flat }; // Добавление адреса
                    db.Client_address.Add(new_address);
                    db.SaveChanges();

                    var new_adr_id = db.Client_address.Select(p => p.id_Address).ToList().Last();
                    Loyality_card new_card = null;
                    Client new_client = null;

                    if (New_card.IsChecked == true)
                    {
                        new_card = new Loyality_card() { Loyality_discount = 3, Amount_purshases = 1};  // Добавление новой карты
                        db.Loyality_card.Add(new_card);
                        db.SaveChanges();
                    }

                    var new_card_id = db.Loyality_card.Select(p => p.id_Loyality_card).ToList().Last();

                    if(new_card != null) {  // Добавление нового клиента
                        new_client = new Client() { id_Address = new_adr_id, Name = name, Surname = surname, Patronymic = patronymic, Phone = phone, id_Loyality_card = new_card_id };
                    }
                    else
                    {
                        new_client = new Client() { id_Address = new_adr_id, Name = name, Surname = surname, Patronymic = patronymic, Phone = phone };
                    }

                    db.Clients.Add(new_client);
                    db.SaveChanges();

                    MessageBox.Show("Новый клиент добавлен!", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else MessageBox.Show("Корректно заполните обязательные поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)  // Ввод только цифр в поле номера телефона
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void Flat_PreviewTextInput(object sender, TextCompositionEventArgs e)   // И номера квартиры
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
