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
    public class S3Warehouse : IAWSclouds<Warehouse>
    {
        public Result Add(Warehouse entity)
        {
            string data = entity.WarehouseId + "," + entity.CustomerID + "," + entity.WarehouseName;
            FileManager.Write(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", data);
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", "warehouses3", null, "warehouse"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(Warehouse entity)
        {
            ObservableCollection<Warehouse> collection = GetAll().Data;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.WarehouseId == collection[i].WarehouseId)
                {
                    FileManager.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", i);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", "warehouses3", null, "warehouse"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public DataResult<Warehouse> Get(Func<Warehouse, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", "warehouses3", null, "warehouse");
            if (filter != null)
            {
                ObservableCollection<Warehouse> collection = GetAll(filter).Data;
                return new SuccessDataResult<Warehouse>(collection[0]);
            }
            return new ErrorDataResult<Warehouse>();
        }

        public DataResult<ObservableCollection<Warehouse>> GetAll(Func<Warehouse, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", "warehouses3", null, "warehouse");
            ObservableCollection<Warehouse> Collection = new ObservableCollection<Warehouse>();
            List<string> Lines = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt");
            string[] liste = new string[] { };
            string satir = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                satir = Lines[i];
                if (satir.Trim() != "")
                {
                    liste = satir.Split(",");
                    Warehouse warehouse = new Warehouse { WarehouseId = Convert.ToInt32(liste[0]), CustomerID = Convert.ToInt32(liste[1]), WarehouseName = liste[3] };
                    Collection.Add(warehouse);
                }
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<Warehouse>>(Collection) :
                new SuccessDataResult<ObservableCollection<Warehouse>>((ObservableCollection<Warehouse>)Collection.Where(filter));
        }

        public Result Update(Warehouse entity)
        {
            ObservableCollection<Warehouse> collection = GetAll().Data;
            string data = entity.WarehouseId + "," + entity.CustomerID + "," + entity.WarehouseName;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.WarehouseId == collection[i].WarehouseId)
                {
                    FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", i, data);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\warehouse.txt", "warehouses3", null, "warehouse"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
