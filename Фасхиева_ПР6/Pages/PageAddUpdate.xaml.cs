using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Policy;
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

namespace Фасхиева_ПР6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddUpdate.xaml
    /// </summary>
    public partial class PageAddUpdate : Page
    {
        Group GROUP; // объект, в котором будут хранится данные о новой или отредактированной группе
        bool grAdd = false; // для определения, создаем новый объект или редактируем старый
        private List<Group> group;
        public PageAddUpdate()  // конструктор для создания новой группы (без аргументов)       
        {
            InitializeComponent();
            listFild();
            grAdd = false;
        }
        public void listFild() // метод для заполнения списков
        {
            //cbClient.ItemsSource = DataBase.bd.Clients.ToList();
            //cbClient.SelectedValuePath = "idClient";
            //cbClient.DisplayMemberPath = "NameClient";

            lbInstructors.ItemsSource = DataBase.bd.Instructors.ToList();
            lbClients.ItemsSource = DataBase.bd.Clients.ToList();
        }
       
        public PageAddUpdate(Group group)  // конструктор для редактирования данных о группе ( с аргументом)
        {
            InitializeComponent();

            listFild();
            GROUP = group;
            grAdd = true;
            tbGroup.Text = group.NameGroup; //вывод названия группы
            tbPrice.Text = Convert.ToString(group.price); //вывод цены за одно занятие

            //int str = 0;
            //foreach (SeasonTicket st in ST)
            //{
            //    str = Convert.ToInt32(st.idClient);
            //}
            //cbClient.SelectedIndex = str; //вывод клиента

            List<SeasonTicket> ST = DataBase.bd.SeasonTicket.Where(x => x.idGroup == group.idGroup).ToList(); // находим клиента для той группы, которую мы редактируем
            foreach (Clients client in lbClients.Items) // цикл для выделения клиента группы в общем списке:
            {
                if (ST.FirstOrDefault(x => x.idClient == client.idClient) != null)
                {
                    lbClients.SelectedItems.Add(client);
                }
            }
            int k = 0;
            foreach (SeasonTicket t in ST)
            {
                k = Convert.ToInt32(t.count);                
            }
            tbCount.Text = "" + k; //вывод количества посещений

            // находим инструктора для той группы, которую мы редактируем
            List<Training> TR = DataBase.bd.Training.Where(x=>x.idGroup==group.idGroup).ToList();
            // цикл для выделения инструктора групп в общем списке:
            foreach (Instructors instructors in lbInstructors.Items)
            {
                if (TR.FirstOrDefault(x => x.idInstruct == instructors.idInstruct) != null)
                {
                    lbInstructors.SelectedItems.Add(instructors);
                }
            }
            btAdd.Content = "Сохранить";
            tbHeader.Text = "Изменение данных";            
        }

        public PageAddUpdate(List<Group> group)  
        {
            this.group = group;
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            if (grAdd == false)
            {
                GROUP = new Group();
            }
            //заполняем поля таблицы Group
            GROUP.title = tbGroup.Text;
            GROUP.price = Convert.ToInt32(tbPrice.Text);
            if (grAdd == false)
            {
                DataBase.bd.Group.Add(GROUP);
            }

            //находим список с клиентами для группы
            List<SeasonTicket> ticket = DataBase.bd.SeasonTicket.Where(x => GROUP.idGroup == x.idGroup).ToList();
            //если список не пустой, удаляем из него всех клиентов для этой группы
            if (ticket.Count > 0)
            {
                foreach(SeasonTicket st in ticket)
                {
                    DataBase.bd.SeasonTicket.Remove(st);
                }
            }
            foreach (Clients client in lbClients.SelectedItems)
            {
                SeasonTicket ST = new SeasonTicket()
                {
                    count = Convert.ToInt32(tbCount.Text),
                    idGroup = GROUP.idGroup,
                    idClient = client.idClient,
                    cost = GROUP.price * Convert.ToInt32(tbCount.Text)
                };
                DataBase.bd.SeasonTicket.Add(ST);
            }

            //находим список с инструкторами для группы
            List<Training> training = DataBase.bd.Training.Where(x => GROUP.idGroup == x.idGroup).ToList();
            //если список не пустой, удаляем из него всех инструкторов для этой группы
            if (training.Count > 0)
            {
                foreach (Training t in training)
                {
                    DataBase.bd.Training.Remove(t);
                }
            }

            foreach (Instructors instructors in lbInstructors.SelectedItems)
            {
                Training TR = new Training()
                {
                    idGroup = GROUP.idGroup,
                    idInstruct = instructors.idInstruct,
                };
                DataBase.bd.Training.Add(TR);
            }

            DataBase.bd.SaveChanges();
            MessageBox.Show("Успешное добавление!");
            ClassFrame.frameL.Navigate(new PageGroup());


            //}
            //catch
            //{
            //    MessageBox.Show("Что-то пошло не так..");
            //}
        }
        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup());
        }

        private void imAdd_PreviewMouseUp(object sender, MouseButtonEventArgs e) //переход на страницу добавление инструктора
        {
            ClassFrame.frameL.Navigate(new PageAddInstructors());
        }
}
}
