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
    public class S3Category : IAWSclouds<Category>
    {
        public Result Add(Category entity)
        {
            string data = entity.CategoryId + "," + entity.CategoryName;
            FileManager.Write(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", data);
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", "warehouses3", null, "category"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(Category entity)
        {
            ObservableCollection<Category> collection = GetAll().Data;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.CategoryId == collection[i].CategoryId)
                {
                    FileManager.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", i);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", "warehouses3", null, "category"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public DataResult<Category> Get(Func<Category, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", "warehouses3", null, "category");
            if (filter != null)
            {
                ObservableCollection<Category> collection = GetAll(filter).Data;
                return new SuccessDataResult<Category>(collection[0]);
            }
            return new ErrorDataResult<Category>();
        }

        public DataResult<ObservableCollection<Category>> GetAll(Func<Category, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", "warehouses3", null, "category");
            ObservableCollection<Category> Collection = new ObservableCollection<Category>();
            List<string> Lines = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt");
            string[] liste = new string[] { };
            string satir = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                satir = Lines[i];
                if (satir.Trim() != "")
                {
                    liste = satir.Split(",");
                    Category category = new Category() { CategoryId = Convert.ToInt32(liste[0]), CategoryName = liste[1] };
                    Collection.Add(category);
                }
            }

            return filter == null ? new SuccessDataResult<ObservableCollection<Category>>(Collection) :
                new SuccessDataResult<ObservableCollection<Category>>((ObservableCollection<Category>)Collection.Where(filter));
        }

        public Result Update(Category entity)
        {
            ObservableCollection<Category> collection = GetAll().Data;
            string data = entity.CategoryId + "," + entity.CategoryName;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.CategoryId == collection[i].CategoryId)
                {
                    FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", i, data);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\category.txt", "warehouses3", null, "category"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
