using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class ChangeClient : Window
    {
        int id;

        public ChangeClient()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(cl_id.Text);

                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var ClientExists = db.Clients.Any(p => p.id_Client == id);

                        if (ClientExists)
                        {
                            personal_data.IsEnabled = true;
                            change_btn.IsEnabled = true;
                            cl_founded.Content = "Клиент найден!";
                        }
                        else MessageBox.Show("Клиент с таким id не найден!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }               
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(Exception)
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            string name = cl_name.Text;
            string surname = cl_surname.Text;
            string patronymic = cl_patronymic.Text;
            string phone = cl_phone.Text;
            string country = cl_country.Text;
            string city = cl_city.Text;
            string street = cl_street.Text;
            string building = cl_building.Text;
            string flat = cl_flat.Text;
            string c_id = cl_cardID.Text;
            bool canChange = true;

            using (ADOmodel db = new ADOmodel())
            {
                var client = db.Clients.Where(p => p.id_Client == id).FirstOrDefault();
                var client_address = db.Client_address.Where(p => p.id_Address == db.Clients.Where(c => c.id_Client == id).Select(c => c.id_Address).FirstOrDefault()).FirstOrDefault();

                if (name != "") client.Name = name;
                if (surname != "") client.Surname = surname;
                if (patronymic != "") client.Patronymic = patronymic;

                if (phone != "" && phone.Length == 11) client.Phone = phone;
                else if (phone != "" && phone.Length != 11)
                {
                    MessageBox.Show("Некорректный номер телефона!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    canChange = false;
                }

                if (country != "") client_address.Country = country;
                if (city != "") client_address.City = city;
                if (street != "") client_address.Street = street;
                if (building != "") client_address.Building = building;
                if (flat != "") client_address.Flat = flat;
                
                if (c_id != "")
                {
                    try
                    {
                        int card_id = Convert.ToInt32(c_id);
                        var CardExists = db.Loyality_card.Any(c => c.id_Loyality_card == card_id);

                        if (CardExists) client.id_Loyality_card = card_id;
                        else
                        {
                            MessageBox.Show("Карты с таким идентификатором не существует!\nНеобходимо сначала создать карту.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            canChange = false;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Некорректный id карты!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        canChange = false;
                    } 
                }

                if (canChange)
                {
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }           
            }
        }

        private void cl_phone_PreviewTextInput(object sender, TextCompositionEventArgs e)   // Только цифры в поле ввода телефона
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void cl_flat_PreviewTextInput(object sender, TextCompositionEventArgs e)    // и квартиры
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
