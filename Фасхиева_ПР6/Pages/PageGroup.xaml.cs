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
using Фасхиева_ПР6.Pages;

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
            foreach (SeasonTicket stc in ST)
            {
                cost += Convert.ToInt32(stc.count * stc.Group.price);
            }

            tb.Text = "Стоимость занятий в месяц: " + cost.ToString() + " руб.";       
        }
       
        private void tbCount_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<SeasonTicket> tick = DataBase.bd.SeasonTicket.Where(x => x.idGroup == index).ToList();
            int count = 0;
            foreach (SeasonTicket stc in tick)
            {
                count += Convert.ToInt32(stc.count);
            }
            tb.Text = "Количество посещений: " + count.ToString();
        }

        private void tbTraning_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;  
            int index = Convert.ToInt32(tb.Uid);             
            List<Training> TR = DataBase.bd.Training.Where(x => x.idGroup == index).ToList();
            string str = "";
            foreach (Training t in TR)
            {
                str += t.Instructors.surname + " " + t.Instructors.name + " " + t.Instructors.patronimyc + " ";
            }

            tb.Text = "Инструктор: " + str.Substring(0, str.Length);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAddUpdate());
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e) //кнопка удаления группы
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Group group = DataBase.bd.Group.FirstOrDefault(x => x.idGroup == index);
            DataBase.bd.Group.Remove(group);
            DataBase.bd.SaveChanges();
            ClassFrame.frameL.Navigate(new PageGroup()); //перезагрузка страницы
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e) //переход на страницу редактирования данных
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Group group = DataBase.bd.Group.FirstOrDefault(x=>x.idGroup == index);
            ClassFrame.frameL.Navigate(new PageAddUpdate(group));
        }

        private void tbClient_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<SeasonTicket> tick = DataBase.bd.SeasonTicket.Where(x => x.idGroup == index).ToList();
            string strCl = "";
            foreach(SeasonTicket t in tick)
            {
                strCl+=t.Clients.surname + " " + t.Clients.name + " " + t.Clients.patronimyc + " ";
            }
            tb.Text = "Клиент: " + strCl.Substring(0, strCl.Length);
        }
    }
}
