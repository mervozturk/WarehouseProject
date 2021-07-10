﻿using Core.FileHelper;
using Core.Results;
using DataAccess.Abstact;
using DataAccess.AWSclouds.RDS;
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
            UId = FileManager.Read();
            _awsCategory = new RDSCategory();
            _awsProduct = new RDSProduct();
            _awsWarehouse = new RDSWarehouse();
            _awsUser = new RDSUser();
            Productlist.ItemsSource = ItemList();
            
        }

        public ObservableCollection<ProductDTO> ItemList()
        {
            DataResult<User> result = _awsUser.Get("SELECT * FROM Warehouse.Users Where ID = '" + UId + "'");
            user = result.Data;
            if (!result.Success)
            {
                MessageBox.Show("hata");
            }
            ObservableCollection<ProductDTO> productList = new ObservableCollection<ProductDTO>();
            products = _awsProduct.GetAll().Data;                                        
            categories = _awsCategory.GetAll().Data;
            warehouses = _awsWarehouse.GetAll().Data;

            var WarehouseList = from w in warehouses
                                where w.CustomerID == user.CustomerId
                                select new Warehouse
                                {
                                    CustomerID = w.CustomerID,
                                    WarehouseId = w.WarehouseId,
                                    WarehouseName = w.WarehouseName
                                };

            for (int i = 0; i < products.Count; i++)
            {
                if (WarehouseList.Any(w => w.WarehouseId == products[i].WarehouseID))
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