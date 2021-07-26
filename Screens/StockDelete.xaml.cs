using Core.FileHelper;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for StockDelete.xaml
    /// </summary>
    public partial class StockDelete : UserControl
    {
        int UId;
        User user;

        IAWSclouds<Category> _awsCategory;
        IAWSclouds<Warehouse> _awsWarehouse;
        IAWSclouds<User> _awsUser;
        IAWSclouds<Product> _awsProduct;
        ObservableCollection<Category> categories;
        ObservableCollection<Warehouse> warehouses;
        ObservableCollection<Product> products;
        public StockDelete()
        {
            InitializeComponent();
            UId = FileManager.Read();

            _awsCategory = new RDSCategory();
            _awsWarehouse = new RDSWarehouse();
            _awsUser = new RDSUser();
            _awsProduct = new RDSProduct();

            user = _awsUser.Get("SELECT* FROM Warehouse.Users Where ID='" + UId + "'").Data;
            warehouses = _awsWarehouse.GetAll("SELECT* FROM Warehouse.Warehouses Where CustomerID='" + user.CustomerId + "'").Data;
            categories = _awsCategory.GetAll("SELECT* FROM Warehouse.Categorys").Data;
            products = new ObservableCollection<Product>();

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

        public ObservableCollection<string> ProductList(ObservableCollection<Product> products)
        {
            ObservableCollection<string> productName = new ObservableCollection<string>();

            for (int i = 0; i < products.Count; i++)
            {
                productName.Add(products[i].ProductName + "-" + products[i].Description);
            }
            return productName;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem==null || ProductComboBox.SelectedItem==null || WarehouseComboBox.SelectedItem==null)
            {
                MessageBox.Show("boş alan bırakmayınız");
            }
            else
            {
                Product product = products[ProductComboBox.SelectedIndex];


                if (product.UnitsInStock < 0)
                {
                    if (_awsProduct.Delete(product).Success)
                    {
                        MessageBox.Show("ürün silindi");
                        CategoryComboBox.SelectedItem = null;
                        ProductComboBox.SelectedItem = null;
                        WarehouseComboBox.SelectedItem = null;
                    }
                }
                else
                {
                    MessageBox.Show("Stokta ürün var");
                }
            }
        }
        public void ComboBoxSelectionChanged()
        {
            if (CategoryComboBox.SelectedItem != null && WarehouseComboBox.SelectedItem != null)
            {
                ObservableCollection<Product> productsUID = new ObservableCollection<Product>();
                Warehouse warehouse = warehouses.Single(w => w.WarehouseName == WarehouseComboBox.SelectedItem.ToString());
                Category category = categories.Single(c => c.CategoryName == CategoryComboBox.SelectedItem.ToString());
                products = _awsProduct.GetAll("SELECT* FROM Warehouse.Products").Data;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].WarehouseID == warehouse.WarehouseId && products[i].CategoryId == category.CategoryId)
                    {
                        productsUID.Add(products[i]);
                    }
                }
                ProductComboBox.ItemsSource = ProductList(productsUID);
            }
        }
        private void WarehouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelectionChanged();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelectionChanged();
        }
       
    }
}
