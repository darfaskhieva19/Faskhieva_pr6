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
    /// Логика взаимодействия для PageGroup.xaml
    /// </summary>
    public partial class PageGroup : Page
    {
        List<Group> groups = DataBase.bd.Group.ToList();
        public PageGroup()
        {
            InitializeComponent();
            lGroup.ItemsSource = groups;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new MenuAdmin());
        }

        private void tbCost_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<SeasonTicket> ST = DataBase.bd.SeasonTicket.Where(x => x.idGroup == index).ToList();
            int cost = 0;
            foreach (SeasonTicket st in ST)
            {
                //cost += Convert.ToInt32(st.count * st.prise);
            }
            tb.Text = "Стоимость в месяц: " + cost.ToString() + " руб.";         
        }

        private void tbInstruction_Loaded(object sender, RoutedEventArgs e)
        {
            //TextBlock tb = (TextBlock)sender;
            //int id = Convert.ToInt32(tb.Uid);
            //Training train = DataBase.bd.Training.FirstOrDefault(x => x.idInstruct == id);

            //if (train != null)
            //{
            //    tb.Text = "Инструктор: ";
            //    tb.Text += train.Instructors.DataInstruct;
            //}
            //else
            //{
            //    tb.Visibility = Visibility.Collapsed;
            //}
        }
    }
}
