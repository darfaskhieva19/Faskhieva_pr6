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

namespace Фасхиева_ПР6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePersonal.xaml
    /// </summary>
    public partial class PagePersonal : Page
    {
        //Clients user;
        public PagePersonal()
        {
            InitializeComponent();
            //this.user = user;
            //tbSurname.Text = user.surname;
            //tbName.Text = user.name;
            //tbPatronimyc.Text = user.patronimyc;
            //tbGender.Text = user.Genders.gender;
            //tbRole.Text = user.Role.role1;
            //tbPhone.Text = user.phone;
            //tbBirthday.Text = user.birthday.ToString("dd MMMM yyyy");
            //if (tbPatronimyc.Text != " ")
            //{
            //    tbPatronimyc.Text = user.patronimyc;
            //}
            //else
            //{
            //    tbPatronimyc.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#7be0ad");
            //    tbPatronimyc.Text = "не задано";
            //}
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) //переход на окно для редактирования данных
        {
            //WindowPersonalAccount windowPerson = new WindowPersonalAccount(user);
            //windowPerson.ShowDialog();
            //ClassFrame.frameL.Navigate(new PagePersonal(user));
        }

        private void btnLoginPassword_Click(object sender, RoutedEventArgs e) //переход на окно для изменения логина и пароля
        {
            //WindowUpdateLoginPassword windLogPass = new WindowUpdateLoginPassword(user);
            //windLogPass.ShowDialog();
            //ClassFrame.frameL.Navigate(new PagePersonal(user));
        }

        private void btnChooseImage_Click(object sender, RoutedEventArgs e) //выбор фотографии
        {

        }
    }
}
