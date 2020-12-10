﻿using System;
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
    public partial class ChangeEmployee : Window
    {
        int id;
        public ChangeEmployee()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(empl_id.Text);
                if (id > 0)
                {
                    using (ADOmodel db = new ADOmodel())
                    {
                        var EmployeeExists = db.Employees.Any(em => em.id_Employee == id);
                        if (EmployeeExists)
                        {
                            empl_data.IsEnabled = true;
                            change_btn.IsEnabled = true;
                            empl_found.Content = "Сотрудник с таким id найден!";
                        }
                        else MessageBox.Show("Сотрудник с таким id не найден!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный id!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            string name = empl_Name.Text;
            string surname = empl_Surname.Text;
            string patronymic = empl_Patronymic.Text;
            var pos = empl_Position.SelectedIndex;
            var role = empl_Role.SelectedIndex;
            string log = empl_Log.Text;
            string pas = empl_Password.Text;

            using (ADOmodel db = new ADOmodel())
            {
                var employee = db.Employees.Where(em => em.id_Employee == id).FirstOrDefault();

                if (name != "") employee.Name = name;
                if (surname != "") employee.Surname = surname;
                if (patronymic != "") employee.Patronymic = patronymic;
                if (pos != -1) employee.id_Position = pos + 1;
                if (role != -1) employee.id_Role = role + 1;
                if (log != "") employee.Login = log;
                if (pas != "") employee.Password = pas;

                db.SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
