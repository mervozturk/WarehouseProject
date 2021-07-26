using Core.FileHelper;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
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
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        IAWSclouds<Category> _awsCategory;
        public AddCategory()
        {
            InitializeComponent();
            _awsCategory = new RDSCategory();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryName.Text != null)
            {
                if (_awsCategory.Add(new Category { CategoryName = CategoryName.Text }).Success)
                {
                    MessageBox.Show("Kategori kaydedildi");
                    base.Close();
                }

            }
        }
    }
}
