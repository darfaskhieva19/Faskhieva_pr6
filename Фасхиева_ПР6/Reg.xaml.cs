using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }    

        bool Telefon(string phone)
        {
            Regex ph = new Regex("^[8][(](\\d{3})[)]\\d{3}-\\d{2}-\\d{2}$");
            if (ph.IsMatch(phone))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите номер телефона корректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        bool IsLogin(string login)
        {
            Clients log = DataBase.bd.Clients.FirstOrDefault(x => x.login == login);
            if (log == null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Логин занят!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        //bool IsPass(string password)
        //{
            //Regex r = new Regex("(?=.*[A-Z])");
            //Regex r1 = new Regex("[a-z].*[a-z].*[a-z]");
            //Regex r2 = new Regex("\\d.*\\d");
            //Regex r3 = new Regex("[!@#$%^&*()_+=]");
            //if (r.IsMatch(tbPassword.Password) == true)
            //{
            //    if (r1.IsMatch(tbPassword.Password) == true)
            //    {
            //        if (r2.IsMatch(tbPassword.Password) == true)
            //        {
            //            if (r3.IsMatch(tbPassword.Password) == true)
            //            {
            //                if (password.Length >= 8)
            //                {
            //                    return true;
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Общая длина пароля не менее 8 символов!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            //                    return false;
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("Пароль должен содержать не менее 1 спец. символа!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            //                return false;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Пароль должен содержать не менее 2 цифры!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Пароль должен содержать не менее 3 строчных латинских символов!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return false;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Пароль должен содержать не менее 1 заглавного латинского символа!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return false;
            //}
        //}

        private void btReg_Click(object sender, RoutedEventArgs e)
        {
            int gender = 0;
            if (RBWoman.IsChecked == true)
            {
                gender = 1;
            }
            else if (RBMan.IsChecked == true)
            {
                gender = 2;
            }

            int pas = tbPassword.Password.GetHashCode();
            Clients log = DataBase.bd.Clients.FirstOrDefault(x => x.login == tbLogin.Text);
            if (log == null)
            {
                Clients client = new Clients()
                {
                    surname = Surname.Text,
                    name = Name.Text,
                    patronimyc = Patronimyc.Text,
                    phone = Phone.Text,
                    birthday = (DateTime)Birthday.SelectedDate,
                    idGender = gender,
                    login = tbLogin.Text,
                    password = tbPassword.Password.GetHashCode(),
                    idRole = 2
                };
                DataBase.bd.Clients.Add(client);
                DataBase.bd.SaveChanges();
                MessageBox.Show("Вы зарегистрированы!");
            }
            else
            {
                MessageBox.Show("Такой логин уже существует! Придумайте другой логин!");
            }

            //if (Surname.Text == "" || Name.Text == "" || Patronimyc.Text == "" || Birthday.Text == "" || Phone.Text == "" || tbLogin.Text == "" || tbPassword.Password == "")
            //{
            //    MessageBox.Show("Заполните поля!", "Регистрация", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            //}
            //else
            //{
            //    if (IsLogin(tbLogin.Text))
            //    {
            //        if (Telefon(Phone.Text))
            //        {
            //            if (IsPass(tbPassword.Password))
            //            {
            //                int gender = 0;
            //                if (RBWoman.IsChecked == true)
            //                {
            //                    gender = 1;
            //                }
            //                else if (RBMan.IsChecked == true)
            //                {
            //                    gender = 2;
            //                }
            //                if (rbClient.IsChecked == true)
            //                {
            //                    Logins log = new Logins()
            //                    {
            //                        login = tbLogin.Text,
            //                        password = tbPassword.Password.GetHashCode(),
            //                        idRole = 3
            //                    };
            //                    DataBase.bd.Logins.Add(log);
            //                    DataBase.bd.SaveChanges();
            //                    Clients client = new Clients()
            //                    {
            //                        idClient = log.kodUsers,
            //                        surname = Surname.Text,
            //                        name = Name.Text,
            //                        patronimyc = Patronimyc.Text,
            //                        phone = Phone.Text,
            //                        birthday = (DateTime)Birthday.SelectedDate,
            //                        idGender = gender,
            //                    };
            //                    DataBase.bd.Clients.Add(client);
            //                    DataBase.bd.SaveChanges();
            //                    MessageBox.Show("Успешная регистрация!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            //                }
            //                else
            //                {
            //                    if (IsInstuct(cbCategory.SelectedIndex, cbEducation.SelectedIndex))
            //                    {
            //                        Logins log = new Logins()
            //                        {
            //                            login = tbLogin.Text,
            //                            password = tbPassword.Password.GetHashCode(),
            //                            idRole = 3
            //                        };
            //                        DataBase.bd.Logins.Add(log);
            //                        DataBase.bd.SaveChanges();
            //                        Instructors instructors = new Instructors()
            //                        {
            //                            idInstruct = log.kodUsers,
            //                            surname = Surname.Text,
            //                            name = Name.Text,
            //                            patronimyc = Patronimyc.Text,
            //                            phone = Phone.Text,
            //                            idCategory = (int)cbCategory.SelectedValue,
            //                            idEducation = (int)cbEducation.SelectedValue,
            //                            idGender = gender,
            //                            birthday = (DateTime)Birthday.SelectedDate,
            //                        };
            //                        DataBase.bd.Instructors.Add(instructors);
            //                        DataBase.bd.SaveChanges();
            //                        PassportData pasport = new PassportData()
            //                        {
            //                            idPassport = instructors.idInstruct,
            //                            seria = Convert.ToInt32(Seria.Text),
            //                            nomer = Convert.ToInt32(Number.Text),
            //                            division_code = DivisionCode.Text,
            //                        };
            //                        DataBase.bd.PassportData.Add(pasport);
            //                        DataBase.bd.SaveChanges();
            //                        MessageBox.Show("Успешная регистрация!", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
