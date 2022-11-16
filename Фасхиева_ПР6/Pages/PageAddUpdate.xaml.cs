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

namespace Фасхиева_ПР6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddUpdate.xaml
    /// </summary>
    public partial class PageAddUpdate : Page
    {
        Group GROUP;
        bool grAdd = false;
        public PageAddUpdate()
        {
            InitializeComponent();
            listFild();
            grAdd = true;
        }
        public void listFild() // метод для заполнения списков
        {
            cbInstructor.ItemsSource = DataBase.bd.Instructors.ToList();
            cbInstructor.SelectedValuePath = "idInstruct";
            cbInstructor.DisplayMemberPath = "NameInst";

            cbClient.ItemsSource = DataBase.bd.Clients.ToList();
            cbClient.SelectedValuePath = "idClient";
            cbClient.DisplayMemberPath = "NameClient";

            cbEducation.ItemsSource = DataBase.bd.Education.ToList();
            cbEducation.SelectedValuePath = "idEducation";
            cbEducation.DisplayMemberPath = "education1";

            cbCategory.ItemsSource = DataBase.bd.Category.ToList();
            cbCategory.SelectedValuePath = "idCategory";
            cbCategory.DisplayMemberPath = "category1";

            cbPost.ItemsSource = DataBase.bd.Posts.ToList();
            cbPost.SelectedValuePath = "idPost";
            cbPost.DisplayMemberPath = "post";
        }
        //public PageAddUpdate(Group group) //редактирование
        //{
        //    InitializeComponent();
        //    this.User = user;
        //    listFild();
        //    GROUP = group;
        //    grAdd = false;
        //}

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup());
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            if (grAdd == false)
            {
                GROUP = new Group();
            }
            GROUP.title = tbGroup.Text;
            GROUP.price = Convert.ToInt32(tbPrice.Text);
            if (grAdd == false)
            {
                DataBase.bd.Group.Add(GROUP);
            }

            SeasonTicket ticket = new SeasonTicket()
            {
                count = Convert.ToInt32(tbCount.Text),
                idGroup = GROUP.idGroup,
                idClient = cbClient.SelectedIndex + 1,
                //cost = GROUP.price * count
            };
            if (grAdd == false)
            {
                DataBase.bd.SeasonTicket.Add(ticket);
            }

            if (cbInstructor.SelectedValue == null)
            {
                Instructors instructor = new Instructors()
                {
                    surname = tbSurname.Text,
                    name = tbName.Text,
                    patronimyc = tbPatronimyc.Text,
                    phone = tbPhone.Text,
                    idCategory = cbCategory.SelectedIndex + 1,
                    idEducation = cbEducation.SelectedIndex + 1,
                    idPost = cbPost.SelectedIndex + 1,
                };
                if (grAdd == true)
                {
                    DataBase.bd.Group.Add(GROUP);
                }
            }
            else
            {
                List<Instructors> INST = DataBase.bd.Instructors.Where(x => GROUP.idGroup == x.idInstruct).ToList();

                foreach (Instructors inst in INST)
                {
                    Instructors instructors = new Instructors()
                    {
                        surname = tbSurname.Text,
                        name = tbName.Text,
                        patronimyc = tbPatronimyc.Text,
                        phone = tbPhone.Text,
                        idCategory = cbCategory.SelectedIndex + 1,
                        idEducation = cbEducation.SelectedIndex + 1,
                        idPost = cbPost.SelectedIndex + 1,
                    };
                    DataBase.bd.Instructors.Add(instructors);
                }
                if (grAdd == true)
                {
                    DataBase.bd.Group.Add(GROUP);
                }
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

        private void imAdd_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            spNewInstructors.Visibility = Visibility.Visible;
            wpNewInstructors.Visibility = Visibility.Visible;
        }
    }
}
