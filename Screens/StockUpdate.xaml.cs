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
    /// Interaction logic for StockUpdate.xaml
    /// </summary>
    public partial class StockUpdate : UserControl
    {

        int UId;
        User user;

        IAWSclouds<Warehouse> _awsWarehouse;
        IAWSclouds<User> _awsUser;
        IAWSclouds<Product> _awsProduct;
        IAWSclouds<Category> _awsCategory;

        ObservableCollection<Warehouse> warehouses;
        ObservableCollection<Product> products;
        ObservableCollection<Category> categories;

        Category _category;
        Warehouse _warehouse;
        Product _product;

        public StockUpdate()
        {
            InitializeComponent(); UId = FileManager.Read();

            _awsWarehouse = new RDSWarehouse();
            _awsUser = new RDSUser();
            _awsProduct = new RDSProduct();
            _awsCategory = new RDSCategory();

            user = _awsUser.Get("SELECT* FROM Warehouse.Users Where ID='" + UId + "'").Data;
            warehouses = _awsWarehouse.GetAll("SELECT* FROM Warehouse.Warehouses Where CustomerID='" + user.CustomerId + "'").Data;
            products = GetProducts();
            categories = _awsCategory.GetAll("SELECT * FROM Warehouse.Categorys").Data;

            ProductComboBox.ItemsSource = ProductList();
            CategoryComboBox.ItemsSource = CategoryList();
            WarehouseComboBox.ItemsSource = WarehouseList();

        }
        public ObservableCollection<string> ProductList()
        {
            ObservableCollection<string> productName = new ObservableCollection<string>();

            for (int i = 0; i < products.Count; i++)
            {
                productName.Add(products[i].ProductName + "-" + products[i].Description);
            }
            return productName;
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
        public ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> productsUID = new ObservableCollection<Product>();
            products = _awsProduct.GetAll("SELECT* FROM Warehouse.Products").Data;
            for (int i = 0; i < products.Count; i++)
            {
                if (warehouses.Any(w => w.WarehouseId == products[i].WarehouseID))
                {
                    productsUID.Add(products[i]);
                }
            }
            return productsUID;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem != null)
            {
                Product product = new Product
                {
                    Id=_product.Id,
                    CategoryId = _category.CategoryId,
                    WarehouseID = _warehouse.WarehouseId,
                    ProductName = NameTxt.Text,
                    UnitsInStock = Convert.ToInt32(UnitsInStockTxt.Text),
                    UnitPrice = Convert.ToDouble(PriceTxt.Text),
                    Description = DescriptionTxt.Text
                };
                if (_awsProduct.Update(product).Success)
                {
                    ProductComboBox.SelectedItem = null;
                    CategoryComboBox.SelectedItem = null;
                    WarehouseComboBox.SelectedItem = null;
                    NameTxt.Text = null;
                    PriceTxt.Text = null;
                    UnitsInStockTxt.Text = null;
                    DescriptionTxt.Text = null;
                    MessageBox.Show("Ürün güncellendi");
                }

            }   
        }
        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductComboBox.SelectedItem != null)
            {
                _product = products[ProductComboBox.SelectedIndex];
                _category = categories.Single(c => c.CategoryId == _product.CategoryId);
                _warehouse = warehouses.Single(w => w.WarehouseId == _product.WarehouseID);
                CategoryComboBox.SelectedItem = _category.CategoryName;
                WarehouseComboBox.SelectedItem = _warehouse.WarehouseName;
                NameTxt.Text = _product.ProductName;
                PriceTxt.Text = _product.UnitPrice.ToString();
                UnitsInStockTxt.Text = _product.UnitsInStock.ToString();
                DescriptionTxt.Text = _product.Description;

            }
        }

    }
 
}
