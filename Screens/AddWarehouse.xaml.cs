using Core.FileHelper;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using DataAccess.AWSclouds.S3;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Screens
{
    /// <summary>
    /// Interaction logic for AddWarehouse.xaml
    /// </summary>
    public partial class AddWarehouse : Window
    {
        int UID;
        User user;

        IAWSclouds<User> _awsUser;
        IAWSclouds<Warehouse> _awsWarehouse;
        public AddWarehouse()
        {
            InitializeComponent();
            string Dbase = FileManager.ReadDatabase();
            if (Dbase == "RDS")
            {
                _awsWarehouse = new RDSWarehouse();
                _awsUser = new RDSUser();
            }
            else if (Dbase == "S3")
            {
                _awsWarehouse = new S3Warehouse();
                _awsUser = new S3User();
            }

            UID = FileManager.ReadUID();
            user = _awsUser.Get(u=>u.Id==UID).Data;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseName.Text!=null)
            {
                if(_awsWarehouse.Add(new Warehouse { WarehouseName = WarehouseName.Text, CustomerID = user.CustomerId }).Success)
                {
                    MessageBox.Show("Depo kaydedildi");
                    base.Close();
                }

            }
        }
    }
}
