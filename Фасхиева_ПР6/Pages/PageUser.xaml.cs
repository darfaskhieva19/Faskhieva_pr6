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
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        Clients user;
        public PageUser(Clients user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateData_Click(object sender, RoutedEventArgs e) //обновление данных
        {
            WindowPersonalAccount windowPers = new WindowPersonalAccount(user);
            windowPers.ShowDialog();
            ClassFrame.frameL.Navigate(new PageUser(user));
        }

        private void btnUpdateLoginPass_Click(object sender, RoutedEventArgs e) //смена пароля
        {
            WindowUpdateLoginPassword windowLogPassword = new WindowUpdateLoginPassword(user);
            windowLogPassword.ShowDialog();
            ClassFrame.frameL.Navigate(new PageUser(user));
        }
    }
}
