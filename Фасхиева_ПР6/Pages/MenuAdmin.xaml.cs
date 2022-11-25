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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : Page
    {
        Clients user; // создаем объект для хранения информации о пользователе
        public MenuAdmin(Clients user)
        {
            InitializeComponent();
            this.user = user;
            tbSurname.Text = tbSurname.Text + user.surname;
            tbName.Text = tbName.Text + user.name;
            tbPatronimyc.Text = tbPatronimyc.Text + user.patronimyc;
            tbPhone.Text = tbPhone.Text + user.phone;
            tbBirthday.Text = tbBirthday.Text + user.birthday.ToString("dd MMMM yyyy");
        }

        private void btGroup_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup(user)); 
        }

        private void btUsers_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAdmin(user));
        }

        private void btUpdateData_Click(object sender, RoutedEventArgs e) //обновление данных
        {
            WindowPersonalAccount windowPerson = new WindowPersonalAccount(user);
            windowPerson.ShowDialog();
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }

        private void btnUpdateLogPass_Click(object sender, RoutedEventArgs e) //смена пароля
        {
            WindowUpdateLoginPassword windowLogPass = new WindowUpdateLoginPassword(user);
            windowLogPass.ShowDialog();
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }
    }
}
