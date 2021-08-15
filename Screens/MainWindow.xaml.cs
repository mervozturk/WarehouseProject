using Core.FileHelper;
using Core.Hashing;
using Core.Results;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using DataAccess.AWSclouds.S3;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IAWSclouds<User> _awsCloud;
        ObservableCollection<string> database;
        public MainWindow()
        {
            InitializeComponent();
            database = new ObservableCollection<string> { "RDS", "S3" };
            SelectDatabase.ItemsSource = database;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Dbase = FileManager.ReadDatabase();
            if (Dbase=="RDS")
            {
                _awsCloud = new RDSUser();
            }
            else if (Dbase == "S3")
            {
                _awsCloud = new S3User();
            }

            if (txtEmail.Text == "" || password.Password == "")
            {
                MessageBox.Show("Boş alan bırakmayınız!");
            }

            DataResult<User> result = _awsCloud.Get(u=>u.Email==txtEmail.Text);
            if (result.Success)
            {
                User user = result.Data;
                //byte[] passwordHash = user.PasswordHash;
                //byte[] passwordSalt = user.PasswordSalt;
                //string checkPassword = password.Password;
                //if (HashingHelper.VerifyPasswordHash(checkPassword, passwordHash, passwordSalt))
                //{
                //    HomePage homePage = new HomePage();
                //    homePage.Show();
                //    base.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Şifre Doğrulanamadı!");
                //}
                if (user.Password == password.Password)
                {
                    FileManager.WriteUID(user.Id);
                    HomePage homePage = new HomePage();
                    homePage.Show();
                    base.Close();
                }

            }
            else
            {
                MessageBox.Show("Email kullanılmamaktadır.");
            }

        }

        private void SelectDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectDatabase.SelectedIndex>0)
            {
                FileManager.WriteDatabase(database[SelectDatabase.SelectedIndex]);
            }
        }
    }
}
