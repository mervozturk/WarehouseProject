using Core.FileHelper;
using Core.Hashing;
using Core.Results;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using Entities;
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

namespace Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IAWSclouds<User> _awsCloud;
        public MainWindow()
        {
            InitializeComponent();
            _awsCloud = new RDSUser();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text==""  || password.Password == "")
            {
                MessageBox.Show("Boş alan bırakmayınız!");
            }

            DataResult<User> result= _awsCloud.Get("SELECT * FROM Warehouse.Users Where Email='" + txtEmail.Text.ToString() + "'");
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
                    FileManager.Write(user.Id);
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
    }
}
