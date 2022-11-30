using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        Clients user;
        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }

        public PageUser(Clients user)
        {
            InitializeComponent();
            this.user = user;
            tbSurname.Text = tbSurname.Text + user.surname;
            tbName.Text = tbName.Text + user.name;
            tbPatronimyc.Text = tbPatronimyc.Text + user.patronimyc;
            tbPhone.Text = tbPhone.Text + user.phone;
            tbBirthday.Text = tbBirthday.Text + user.birthday.ToString("dd MMMM yyyy");

            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            if (p.Count != 0)
            {
                byte[] Bar = p[p.Count - 1].photoBinary;
                showImage(Bar, imClient);
            }
        }

        private void btnUpdateData_Click(object sender, RoutedEventArgs e) //обновление данных
        {
            WindowPersonalAccount windowPers = new WindowPersonalAccount(user);
            windowPers.ShowDialog();
            ClassFrame.frameL.Navigate(new PageUser(user));
        }

        private void btnUpdateLoginPass_Click(object sender, RoutedEventArgs e) //смена пароля
        {
            WindowUpdateLoginPassword windowLogPassword = new WindowUpdateLoginPassword(user);
            windowLogPassword.ShowDialog();
            ClassFrame.frameL.Navigate(new PageUser(user));
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e) //добавление фотографии
        {
            try
            {
                ClientPhoto photo = new ClientPhoto();
                photo.idClient = user.idClient;
                OpenFileDialog OFD = new OpenFileDialog();
                string path = OFD.FileName;
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);
                ImageConverter IC = new ImageConverter();
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                photo.photoBinary = Barray;
                DataBase.bd.ClientPhoto.Add(photo);
                DataBase.bd.SaveChanges();
                MessageBox.Show("Успешное добавление фото!");
                ClassFrame.frameL.Navigate(new PageUser(user));
            }
            catch
            {
                MessageBox.Show("Ошибка с добавлением фото!");
            }
        }
        private void ChoosePhoto_Click(object sender, RoutedEventArgs e) //отображение галлереи
        {
            MessageBoxResult result = MessageBox.Show("Хотите ли Вы добавить новое фото?", "Обновление фотографии", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes) //если пользователь хочет изменить фото, то добавляет новое фото
            {
                try
                {
                    ClientPhoto photo = new ClientPhoto();
                    photo.idClient = user.idClient;
                    OpenFileDialog OFD = new OpenFileDialog();
                    string path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);
                    ImageConverter IC = new ImageConverter();
                    byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    photo.photoBinary = Barray;
                    DataBase.bd.ClientPhoto.Add(photo);
                    DataBase.bd.SaveChanges();
                    MessageBox.Show("Успешное добавление фото!");
                    ClassFrame.frameL.Navigate(new PageUser(user));
                }
                catch
                {
                    MessageBox.Show("Ошибка с добавлением фото!");
                }
            }
            else if(result == MessageBoxResult.No) //если пользовательне хочет добавлять новое фото, то появляется галерея, из которой можно выбрать фотографию
            {
                List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
                if (p.Count != 0)
                {
                    UpdatesPhoto_Click();
                }
                else
                {
                    MessageBox.Show("У пользователя отсутствуют картинки в базе данных!");
                }
            }
        }        
        void UpdatesPhoto_Click()
        {
            spGallery.Visibility = Visibility.Visible;
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            if (p.Count != 0)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {
                byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                showImage(Bar, imGallery);  // отображаем картинку
            }
        }

        int n = 0;
        private void btnBack_Click(object sender, RoutedEventArgs e) //кнопка Назад для фотогаллереи
        {
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            if (p != null)
            {
                if (n == 0)
                {
                    n = p.Count;
                }
                if (n == -1)
                {
                    n = p.Count - 1;
                }
                n--;
                byte[] Bar = p[n].photoBinary;
                BitmapImage BI = new BitmapImage();
                showImage(Bar, imGallery);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) //кнопка Вперед для фотогаллереи
        {
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            n++;
            if (p != null)
            {

                byte[] Bar = p[n].photoBinary;
                showImage(Bar, imGallery);
            }
            if (n == p.Count - 1)
            {
                n = -1;
            }
        }

        private void AddPhotos_Click(object sender, RoutedEventArgs e) //добавление нескольких фотографий в базу
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Multiselect = true;
                if (OFD.ShowDialog() == true)
                {
                    foreach (string file in OFD.FileNames)
                    {
                        ClientPhoto photo = new ClientPhoto();
                        photo.idClient = user.idClient;
                        string path = file;
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);
                        ImageConverter IC = new ImageConverter();
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                        photo.photoBinary = Barray;
                        DataBase.bd.ClientPhoto.Add(photo);
                    }
                    DataBase.bd.SaveChanges();
                    MessageBox.Show("Успешное добавление фотографий!");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка с добавлением нескольких фотографий!");
            }
        }

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e) //обновить фотографию на старую
        {
                List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
                byte[] Bar = p[n].photoBinary;
                showImage(Bar, imClient);
        }

        private void Back_Click(object sender, RoutedEventArgs e) 
        {
            ClassFrame.frameL.Navigate(new MenuAdmin(user));     
        }
    }
}

