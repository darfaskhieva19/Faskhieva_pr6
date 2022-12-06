using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
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
        ClassChange pc = new ClassChange();
        Clients user;
        List<Group> listFilter = new List<Group>();
        List<Group> group = DataBase.bd.Group.ToList();
        public PageGroup(Clients user)
        {

            InitializeComponent();

            lGroup.ItemsSource = DataBase.bd.Group.ToList();
            this.user = user;

            List <Instructors> intructor = DataBase.bd.Instructors.ToList();
            cbInstructors.Items.Add("Все инструктора");
            for(int i = 0; i < intructor.Count; i++)
            {
                cbInstructors.Items.Add(intructor[i].surname);
            }
            cbInstructors.SelectedIndex = 0; // выбранное по умолчанию значение в списке с породами котов ("Все клиенты")
            cbSortirovka.SelectedIndex = 0; // выбранное по умолчанию значение в списке с видами сортировки ("Без сортировки")

            pc.CountPage = DataBase.bd.Group.ToList().Count;
            DataContext = pc;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }

        private void tbCost_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<SeasonTicket> ST = DataBase.bd.SeasonTicket.Where(x => x.idGroup == index).ToList();
            int cost = 0;
            foreach (SeasonTicket stc in ST)
            {
                cost += Convert.ToInt32(stc.countVisit * stc.Group.price);
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
                count += Convert.ToInt32(stc.countVisit);
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
            ClassFrame.frameL.Navigate(new PageAddUpdate(user));
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e) //кнопка удаления группы
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Group group = DataBase.bd.Group.FirstOrDefault(x => x.idGroup == index);
            DataBase.bd.Group.Remove(group);
            DataBase.bd.SaveChanges();
            ClassFrame.frameL.Navigate(new PageGroup(user)); //перезагрузка страницы
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e) //переход на страницу редактирования данных
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Group group = DataBase.bd.Group.FirstOrDefault(x=>x.idGroup == index);
            ClassFrame.frameL.Navigate(new PageAddUpdate(user, group));
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
        void Filter() // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Group> listG = DataBase.bd.Group.ToList();
            string instructor = cbInstructors.SelectedValue.ToString();
            int index = cbInstructors.SelectedIndex;
            List<Training> training = DataBase.bd.Training.Where(z=>z.Instructors.surname == instructor).ToList();
            if (index != 0)
            {
                listFilter = new List<Group>();
                foreach(Training tr in training)
                {
                    foreach(Group grp in listG)
                    {
                        if (grp.idGroup == tr.idGroup)
                        {
                            listFilter.Add(grp);
                        }
                    }
                }
            }
            else  // если выбран пункт "Все инструктора", то сбрасываем фильтрацию:
            {
                listFilter = DataBase.bd.Group.ToList();
            }

            // сортировка
            switch (cbSortirovka.SelectedIndex)
            {
                case 1:
                    listFilter.Sort((x, y) => x.NameGroup.CompareTo(y.NameGroup));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.NameGroup.CompareTo(y.NameGroup));
                    listFilter.Reverse();
                    break;
            }

            if ((bool)ckb.IsChecked)
            {
                listFilter = listFilter.Where(x => x.ColorCost == Brushes.Lavender).ToList();
            }

            // поиск совпадений по названию группы
            if (!string.IsNullOrWhiteSpace(tbGroupSearch.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                listFilter = listFilter.Where(x => x.NameGroup.ToLower().Contains(tbGroupSearch.Text.ToLower())).ToList();
            }

            lGroup.ItemsSource = listFilter;
            if (listFilter.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
        }

        private void cbSortirovka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbGroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                case "first":
                    pc.CurrentPage = 1;
                    break;
                case "last":
                    pc.CurrentPage = pc.CountPages;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            lGroup.ItemsSource = listFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
        }

        private void cbInstructors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ckb_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void PageCount_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(PageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = listFilter.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = listFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            lGroup.ItemsSource = listFilter.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pc.CurrentPage = 1; // текущая страница - это страница 1
        }
    }
}
