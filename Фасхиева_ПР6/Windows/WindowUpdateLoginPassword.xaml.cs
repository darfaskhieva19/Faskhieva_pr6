using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для WindowUpdateLoginPassword.xaml
    /// </summary>
    public partial class WindowUpdateLoginPassword : Window
    {
        Clients user;
        public WindowUpdateLoginPassword(Clients user)
        {
            InitializeComponent();
            this.user = user;
            tbLogin.Text = user.login;
        }

        bool IsPass(string password)
        {
            Regex r = new Regex("(?=.*[A-Z])");
            Regex r1 = new Regex("[a-z].*[a-z].*[a-z]");
            Regex r2 = new Regex("\\d.*\\d");
            Regex r3 = new Regex("[!@#$%^&*()_+=]");
            if (r.IsMatch(tbNewPassw.Password) == true)
            {
                if (r1.IsMatch(tbNewPassw.Password) == true)
                {
                    if (r2.IsMatch(tbNewPassw.Password) == true)
                    {
                        if (r3.IsMatch(tbNewPassw.Password) == true)
                        {
                            if (password.Length >= 8)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Общая длина пароля не менее 8 символов!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен содержать не менее 1 спец. символа!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать не менее 2 цифры!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать не менее 3 строчных латинских символов!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Пароль должен содержать не менее 1 заглавного латинского символа!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        bool CoincidencePass(string pass, string newPass)
        {
            if(pass == newPass)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }
        }

        private void tbUpdatePass_Click(object sender, RoutedEventArgs e)
        {
            int pass = tbOldPassw.Password.GetHashCode();
            Clients client = DataBase.bd.Clients.FirstOrDefault(x => x.login == user.login && x.password == pass);
            if (client == null)
            {
                MessageBox.Show("Введенный пароль неправильный", "Смена пароля", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (IsPass(tbNewPassw.Password))
                {
                    if (CoincidencePass(tbNewPassw.Password, tbNewNewPass.Password))
                    {
                        user.password = tbNewNewPass.Password.GetHashCode();
                        user.login = tbLogin.Text;
                        DataBase.bd.SaveChanges();
                        MessageBox.Show("Пароль успешно изменен!");
                        this.Close();
                    }
                }                
            }
        }
    }
}
