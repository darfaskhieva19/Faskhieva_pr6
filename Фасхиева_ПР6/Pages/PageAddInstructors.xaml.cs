using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Фасхиева_ПР6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddInstructors.xaml
    /// </summary>
    public partial class PageAddInstructors : Page
    {
        Clients user;
        public PageAddInstructors(Clients user)
        {
            InitializeComponent();

            cbEducation.ItemsSource = DataBase.bd.Education.ToList();
            cbEducation.SelectedValuePath = "idEducation";
            cbEducation.DisplayMemberPath = "education1";

            cbCategory.ItemsSource = DataBase.bd.Category.ToList();
            cbCategory.SelectedValuePath = "idCategory";
            cbCategory.DisplayMemberPath = "category1";

            cbPost.ItemsSource = DataBase.bd.Posts.ToList();
            cbPost.SelectedValuePath = "idPost";
            cbPost.DisplayMemberPath = "post";

            this.user = user;
        }

        bool Telefon(string phone)
        {
            Regex ph = new Regex("^[8][(](\\d{3})[)]\\d{3}-\\d{2}-\\d{2}$");
            if (ph.IsMatch(phone))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите номер телефона корректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnAddInstr_Click(object sender, RoutedEventArgs e)
        {
            if (Telefon(tbPhone.Text))
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
                DataBase.bd.SaveChanges();
                MessageBox.Show("Успешное добавление!");
                ClassFrame.frameL.Navigate(new PageAddUpdate(user));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAddUpdate(user));
        }
    }
}
