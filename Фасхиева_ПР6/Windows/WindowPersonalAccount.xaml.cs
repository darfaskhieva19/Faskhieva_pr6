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
using System.Windows.Shapes;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для WindowPersonalAccount.xaml
    /// </summary>
    public partial class WindowPersonalAccount : Window
    {
        Clients user; // создаем объект для хранения информации о пользователе
        public WindowPersonalAccount(Clients user)
        {
            InitializeComponent();
            this.user = user;
            tSurname.Text = user.surname;
            tName.Text = user.name;
            tPatronimyc.Text=user.patronimyc;
            tPhone.Text=user.phone;
            Birthday.SelectedDate = user.birthday;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            user.surname = tSurname.Text;
            user.name = tName.Text;
            user.patronimyc=tPatronimyc.Text;
            user.phone= tPhone.Text;
            user.birthday = Convert.ToDateTime(Birthday.SelectedDate);
            DataBase.bd.SaveChanges();
            MessageBox.Show("Успешное изменение данных!");
            this.Close();
        }
    }
}
