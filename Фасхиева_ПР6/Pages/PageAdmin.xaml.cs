using System;
using System.Collections;
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
using System.Xml.Linq;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        Clients user;
        public PageAdmin(Clients user)
        {
            InitializeComponent();
            dgUsers.ItemsSource = DataBase.bd.Clients.ToList();
            this.user = user;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DataBase.bd.Clients.ToList();
        }

        private void btnSortAsc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DataBase.bd.Clients.OrderBy(z => z.surname).ToList();
        }

        private void btnSortDesc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DataBase.bd.Clients.OrderByDescending(z => z.surname).ToList();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbGender.SelectedIndex)
            {
                case 0:
                   dgUsers.ItemsSource = DataBase.bd.Clients.Where(z => z.idGender == 1).ToList();
                    break;
                case 1:
                   dgUsers.ItemsSource = DataBase.bd.Clients.Where(z => z.idGender == 2).ToList();
                    break;
            }
        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            if (tbSurname.Text != "")
            {
                dgUsers.ItemsSource = DataBase.bd.Clients.Where(z => z.surname == tbSurname.Text).ToList();
            }
            else if (tbName.Text != "")
            {
                dgUsers.ItemsSource = DataBase.bd.Clients.Where(z => z.name == tbName.Text).ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }

        private void RBname_Click(object sender, RoutedEventArgs e)
        {
            spSurname.Visibility = Visibility.Collapsed;
            spName.Visibility = Visibility.Visible;
            btnSeach.Visibility = Visibility.Visible;
        }

        private void RBsurname_Click(object sender, RoutedEventArgs e)
        {
            spSurname.Visibility = Visibility.Visible;
            spName.Visibility = Visibility.Collapsed;
            btnSeach.Visibility = Visibility.Visible;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbGender.SelectedIndex = -1;
            tbSurname.Text = "";
            tbName.Text = "";
            dgUsers.ItemsSource = DataBase.bd.Clients.ToList();

        }
    }
}
