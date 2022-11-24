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
    /// Логика взаимодействия для WindowUpdateLoginPassword.xaml
    /// </summary>
    public partial class WindowUpdateLoginPassword : Window
    {
        Clients user;
        public WindowUpdateLoginPassword(Clients user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void tbUpdatePass_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
