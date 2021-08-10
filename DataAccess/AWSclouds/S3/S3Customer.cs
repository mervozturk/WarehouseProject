using Core.FileHelper;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.AWSclouds.S3
{
    public class S3Customer : IAWSclouds<Customer>
    {
        public Result Add(Customer entity)
        {
            string data = entity.Id + "," + entity.CompanyName;
            FileManager.Write(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", data);
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", "warehouses3", null, "customer"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(Customer entity)
        {
            ObservableCollection<Customer> collection = GetAll().Data;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", i);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", "warehouses3", null, "customer"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public DataResult<Customer> Get(Func<Customer, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", "warehouses3", null, "customer");
            if (filter != null)
            {
                ObservableCollection<Customer> collection = GetAll(filter).Data;
                return new SuccessDataResult<Customer>(collection[0]);
            }
            return new ErrorDataResult<Customer>();
        }

        public DataResult<ObservableCollection<Customer>> GetAll(Func<Customer, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", "warehouses3", null, "customer");
            ObservableCollection<Customer> Collection = new ObservableCollection<Customer>();
            List<string> Lines = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt");
            string[] liste = new string[] { };
            string satir = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                satir = Lines[i];
                if (satir.Trim() != "")
                {
                    liste = satir.Split(",");
                    Customer customer = new Customer { Id = Convert.ToInt32(liste[0]), CompanyName = liste[1] };
                    Collection.Add(customer);
                }
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<Customer>>(Collection) :
                 new SuccessDataResult<ObservableCollection<Customer>>((ObservableCollection<Customer>)Collection.Where(filter));
        }

        public Result Update(Customer entity)
        {
            ObservableCollection<Customer> collection = GetAll().Data;
            string data = entity.Id + "," + entity.CompanyName;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", i, data);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\customer.txt", "warehouses3", null, "customer"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
