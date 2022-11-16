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
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void btGroup_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup()); 
        }

        private void btUsers_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAdmin());
        }

        private void btPersonal_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new Pages.PagePersonal());
        }
    }
}
