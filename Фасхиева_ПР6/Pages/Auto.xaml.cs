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
    //логин пользователя - Nikita12
    //пароль - Nikitos31@1218
    //логин администратора - admin, пароль - admin

    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Page
    {
        public Auto()
        {
            InitializeComponent();
            tbLogin.Text = "admin";
            tbPassword.Password = "admin";
        }

        private void btAuto_Click(object sender, RoutedEventArgs e)
        {
            int pass = tbPassword.Password.GetHashCode();
            Clients log = DataBase.bd.Clients.FirstOrDefault(x => x.login == tbLogin.Text && x.password == pass);
            if (log == null)
            {
                MessageBox.Show("Пользователя не существует!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                switch (log.idRole)
                {
                    case 1:
                        ClassFrame.frameL.Navigate(new MenuAdmin(log));
                        MessageBox.Show("Добро пожаловать, администратор!");
                        break;
                    case 2:
                        ClassFrame.frameL.Navigate(new PageUser(log));
                        MessageBox.Show("Добро пожаловать, пользователь!");
                        break;
                }               
            }
        }
    }
}
