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

namespace Фасхиева_ПР6
{
    /// <summary>
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : Page
    {
        Clients user; // создаем объект для хранения информации о пользователе

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

        public MenuAdmin(Clients user)
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
                showImage(Bar, imAdmin);  // отображаем картинку
            }
        }

        private void btGroup_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageGroup(user));
        }

        private void btUsers_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageAdmin(user));
        }

        private void btUpdateData_Click(object sender, RoutedEventArgs e) //обновление данных
        {
            WindowPersonalAccount windowPerson = new WindowPersonalAccount(user);
            windowPerson.ShowDialog();
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }

        private void btnUpdateLogPass_Click(object sender, RoutedEventArgs e) //смена пароля
        {
            WindowUpdateLoginPassword windowLogPass = new WindowUpdateLoginPassword(user);
            windowLogPass.ShowDialog();
            ClassFrame.frameL.Navigate(new MenuAdmin(user));
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e) //добавление фото
        {
            try
            {
                ClientPhoto p = new ClientPhoto();  // создание объекта для добавления записи в таблицу, где хранится фото
                p.idClient = user.idClient;

                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно                                                       
                OFD.ShowDialog();  // открываем диалоговое окно             
                string path = OFD.FileName;  // считываем путь выбранного изображения
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу
                ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                p.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                DataBase.bd.ClientPhoto.Add(p);  // добавляем объект в таблицу БД
                DataBase.bd.SaveChanges();  // созраняем изменения в БД
                MessageBox.Show("Успешное добавление фото!");
                ClassFrame.frameL.Navigate(new MenuAdmin(user)); // перезагружаем страницу
            }
            catch
            {
                MessageBox.Show("Ошибка с добавлением фотографии!");
            }
        }
    }
}
