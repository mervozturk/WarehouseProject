using Core.FileHelper;
using Core.Results;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
using DataAccess.AWSclouds.S3;
using Entities;
using Entities.DTOs;
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
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : UserControl
    {

        IAWSclouds<Product> _awsProduct;
        IAWSclouds<Category> _awsCategory;
        IAWSclouds<Warehouse> _awsWarehouse;
        IAWSclouds<User> _awsUser;
        ObservableCollection<Product> products;
        ObservableCollection<Category> categories;
        ObservableCollection<Warehouse> warehouses;
        int UId;
        User user;
        public StartScreen()
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
           
            Productlist.ItemsSource = ItemList();

        }

        public ObservableCollection<ProductDTO> ItemList()
        {
            DataResult<User> result = _awsUser.Get(u=>u.Id==UId);
            user = result.Data;
            ObservableCollection<ProductDTO> productList = new ObservableCollection<ProductDTO>();
            products = _awsProduct.GetAll().Data;
            categories = _awsCategory.GetAll().Data;
            warehouses = _awsWarehouse.GetAll(W=>W.CustomerID==user.CustomerId).Data;


            for (int i = 0; i < products.Count; i++)
            {
                if (warehouses.Any(w => w.WarehouseId == products[i].WarehouseID))
                {
                    var categoryname = from c in categories
                                       where products[i].CategoryId == c.CategoryId
                                       select c.CategoryName;

                    var Warehousename = from w in warehouses
                                        where products[i].WarehouseID == w.WarehouseId
                                        select w.WarehouseName;
                    ProductDTO productDTO = new ProductDTO
                    {
                        Category = categoryname.First(),
                        WareHouse = Warehousename.First(),
                        ProductName = products[i].ProductName,
                        UnitsInStock = products[i].UnitsInStock,
                        UnitPrice = products[i].UnitPrice,
                        Description = products[i].Description
                    };
                    productList.Add(productDTO);
                }

            }
            return productList;
        }
    }
}
