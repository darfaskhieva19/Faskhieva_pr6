using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

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

            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList(); // для загрузки картинки находим все фото пользователя в таблице, где хранятся фото
            if (p.Count != 0)  // если список с фото не пустой, начинает переводить байтовый массив в изображение
            {
                byte[] Bar = p[p.Count - 1].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imClient);  // отображаем картинку
            }
        }

        private void ChoosePhoto_Click(object sender, RoutedEventArgs e) //отображение галлереи
        {
            spGallery.Visibility = Visibility.Visible;
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            if (p != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {
                byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                showImage(Bar, imGallery);  // отображаем картинку
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

        private void btnUpdPhoto_Click(object sender, RoutedEventArgs e) //Выбор фотографии из галлереи
        {
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
            showImage(Bar, imClient);  // отображаем картинку
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e) //добавление фотографии
        {
            try
            {
                ClientPhoto photo = new ClientPhoto();
                photo.idClient = user.idClient;
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.ShowDialog();
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

        int n = 0;
        private void btnBack_Click(object sender, RoutedEventArgs e) //кнопка Назад для фотогаллереи
        {
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();          
            if (p != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
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
                byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
                showImage(Bar, imGallery);
            }            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) //кнопка Вперед для фотогаллереи
        {
            List<ClientPhoto> p = DataBase.bd.ClientPhoto.Where(x => x.idClient == user.idClient).ToList();
            n++;
            if (p != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {
                byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                showImage(Bar, imGallery);
            }
            if (n == p.Count - 1)
            {
                n = -1;
            }
        }

        private void AddPhotos_Click(object sender, RoutedEventArgs e) //добавление несколькиъ фотографий в базу
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
                if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
                {
                    foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
                    {
                        ClientPhoto p = new ClientPhoto();  // создание объекта для добавления записи в таблицу, где хранится фото
                        p.idClient = user.idClient;  // присваиваем значение полю idUser (id авторизованного пользователя)
                        string path = file;  // считываем путь выбранного изображения
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
                        ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                        p.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                        DataBase.bd.ClientPhoto.Add(p);  // добавляем объект в таблицу БД
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
            byte[] Bar = p[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
            showImage(Bar, imClient);  // отображаем картинку
        }
    }
}
