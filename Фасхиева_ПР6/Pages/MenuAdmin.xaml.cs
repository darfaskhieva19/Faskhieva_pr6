using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (user.idRole == 2)
            {
                btGroup.Visibility = Visibility.Collapsed;
                btUsers.Visibility=Visibility.Collapsed;
            }
            else
            {
                btGroup.Visibility = Visibility.Visible;
                btUsers.Visibility = Visibility.Visible;
            }
        }

        private void btGroup_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup(user));
        }

        private void btUsers_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAdmin(user));
        } 

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageUser(user));
        }
    }
}
