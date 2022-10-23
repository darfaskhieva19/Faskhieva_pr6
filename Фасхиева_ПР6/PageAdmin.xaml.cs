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
        public PageAdmin()
        {
            InitializeComponent();
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
            //dgUser.ItemsSource = DataBase.bd.Clients.OrderByDescending(z => z.surname).ToList();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbGender.SelectedIndex)
            {
                case 0:
                   // dgUser.ItemsSource = DataBase.bd.Clients.Where(z => z.idGender == 1).ToList();
                    break;
                case 1:
                   // dgUser.ItemsSource = DataBase.bd.Clients.Where(z => z.idGender == 2).ToList();
                    break;
            }
        }

        private void cbPoisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            if (cbPoisk.SelectedIndex != -1)
            {
                if (tbSeach.Text != "")
                {
                    //dgInstructions.ItemsSource = DataBase.bd.Instructors.Where(z => z.surname == tbSeach.Text).ToList();
                }
                else
                {
                    //dgInstructions.ItemsSource = DataBase.bd.Instructors.Where(z => z.name == tbSeach.Text).ToList();
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbPoisk.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            tbSeach.Text = "";
        }
    }
}
