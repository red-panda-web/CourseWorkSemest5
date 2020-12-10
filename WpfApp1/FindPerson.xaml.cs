﻿using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class FindPerson : Window
    {
        public FindPerson()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminPage main = this.Owner as AdminPage;
            string phone = Person_phone.Text;
            string name = Person_name.Text;
            string surname = Person_surname.Text;
            string patronymic = Person_partonymic.Text;
            
            using (ADOmodel db = new ADOmodel())
            {
                if (name != "" && surname != "" && patronymic != "")
                {
                    var ClientExists = db.Clients.Any(p => p.Surname == surname && p.Name == name && p.Patronymic == patronymic);

                    if (ClientExists)
                    {
                        main.ClientsTable.ItemsSource = (from c in db.Clients
                                                         join ca in db.Client_address on c.id_Address equals ca.id_Address
                                                         join lc in db.Loyality_card on c.id_Loyality_card equals lc.id_Loyality_card into grouping
                                                         from gr in grouping.DefaultIfEmpty()
                                                         where (c.Name == name) && (c.Surname == surname) && (c.Patronymic == patronymic)
                                                         select new
                                                         {
                                                             id = c.id_Client,
                                                             Surname = c.Surname,
                                                             Name = c.Name,
                                                             Patronymic = c.Patronymic,
                                                             Phone = c.Phone,
                                                             Country = ca.Country,
                                                             City = ca.City,
                                                             Street = ca.Street,
                                                             Building = ca.Building,
                                                             Flat = ca.Flat,
                                                             LoyalityDiscount = gr == null ? 0 : gr.Loyality_discount,
                                                             AmountPurshase = gr == null ? 0 : gr.Amount_purshases
                                                         }).ToList();
                        this.Close();
                    }
                    else MessageBox.Show("Клиент не найден!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (phone != "")
                {
                    var ClientExists = db.Clients.Any(p => p.Phone == phone);
                    if (ClientExists)
                    {
                        main.ClientsTable.ItemsSource = (from c in db.Clients
                                                         join ca in db.Client_address on c.id_Address equals ca.id_Address
                                                         join lc in db.Loyality_card on c.id_Loyality_card equals lc.id_Loyality_card into grouping
                                                         from gr in grouping.DefaultIfEmpty()
                                                         where (c.Phone == phone) 
                                                         select new
                                                         {
                                                             id = c.id_Client,
                                                             Surname = c.Surname,
                                                             Name = c.Name,
                                                             Patronymic = c.Patronymic,
                                                             Phone = c.Phone,
                                                             Country = ca.Country,
                                                             City = ca.City,
                                                             Street = ca.Street,
                                                             Building = ca.Building,
                                                             Flat = ca.Flat,
                                                             LoyalityDiscount = gr == null ? 0 : gr.Loyality_discount,
                                                             AmountPurshase = gr == null ? 0 : gr.Amount_purshases
                                                         }).ToList();
                        this.Close();
                    }
                    else MessageBox.Show("Клиент не найден!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
