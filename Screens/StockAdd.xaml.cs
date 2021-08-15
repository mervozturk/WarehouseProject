using Core.FileHelper;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using DataAccess.AWSclouds.S3;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for StockAdd.xaml
    /// </summary>
    public partial class StockAdd : UserControl
    {
        int UId;
        User user;
        
        IAWSclouds<Category> _awsCategory;
        IAWSclouds<Warehouse> _awsWarehouse;
        IAWSclouds<User> _awsUser;
        IAWSclouds<Product> _awsProduct;
        ObservableCollection<Category> categories;
        ObservableCollection<Warehouse> warehouses;
        public StockAdd()
        {
            InitializeComponent();
            UId = FileManager.ReadUID();
            string Dbase = FileManager.ReadDatabase();
            if (Dbase == "RDS")
            {
                _awsCategory = new RDSCategory();
                _awsWarehouse = new RDSWarehouse();
                _awsUser = new RDSUser();
                _awsProduct = new RDSProduct();
            }
            else if (Dbase == "S3")
            {
                _awsCategory = new S3Category();
                _awsWarehouse = new S3Warehouse();
                _awsUser = new S3User();
                _awsProduct = new S3Product();
            }


            user = _awsUser.Get(u=>u.Id==UId).Data;
            warehouses = _awsWarehouse.GetAll(w=>w.CustomerID==user.CustomerId).Data;
            categories = _awsCategory.GetAll().Data;

            WarehouseComboBox.ItemsSource = WarehouseList();
            CategoryComboBox.ItemsSource = CategoryList();
        }

        public ObservableCollection<string> WarehouseList()
        {
            ObservableCollection<string> warehouseName = new ObservableCollection<string>();
            
            for (int i = 0; i < warehouses.Count; i++)
            {
                warehouseName.Add(warehouses[i].WarehouseName);
            }
            return warehouseName;
        }

        public ObservableCollection<string> CategoryList()
        {
            ObservableCollection<string> categoryName = new ObservableCollection<string>();
            
            for (int i = 0; i < categories.Count; i++)
            {
                categoryName.Add(categories[i].CategoryName);
            }
            return categoryName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(NameTxt.Text!="" || PriceTxt.Text!="" || StockTxt.Text!="" || DescriptionTxt.Text != "" || CategoryComboBox.SelectedIndex>0 || WarehouseComboBox.SelectedIndex>0)
            {
                Category category = categories[CategoryComboBox.SelectedIndex];
                Warehouse warehouse = warehouses[WarehouseComboBox.SelectedIndex];
                Product product = new Product { 
                    CategoryId = category.CategoryId,
                    WarehouseID = warehouse.WarehouseId,
                    ProductName = NameTxt.Text,
                    UnitPrice = Convert.ToInt32(PriceTxt.Text),
                    UnitsInStock = Convert.ToInt32(StockTxt.Text),
                    Description = DescriptionTxt.Text 
                };
                if (_awsProduct.Add(product).Success)
                {
                    CategoryComboBox.SelectedItem = null;
                    WarehouseComboBox.SelectedItem = null;
                    NameTxt.Text = "";
                    PriceTxt.Text = "";
                    StockTxt.Text = "";
                    DescriptionTxt.Text = "";
                    MessageBox.Show("Ürün Kaydedildi");
                }
            }
               
            else
            {
                MessageBox.Show("Boş alan bırakmaınız!");
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }

        private void AddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            AddWarehouse addWarehouse = new AddWarehouse();
            addWarehouse.Show();
        }
    }
}
