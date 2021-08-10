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
    public class S3User : IAWSclouds<User>
    {
        public Result Add(User entity)
        {
            string data = entity.Id + "," + entity.CustomerId + "," + entity.FirstName + "," + entity.LastName + "," + entity.Email + "," + entity.Password;
            FileManager.Write(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", data);
            if(S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", "warehouses3", null, "user"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(User entity)
        {
            ObservableCollection<User> collection = GetAll().Data;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Delete(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", i);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", "warehouses3", null, "user"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public DataResult<User> Get(Func<User, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", "warehouses3", null, "user");
            if (filter!=null)
            {
                ObservableCollection<User> collection = GetAll(filter).Data;
                return new SuccessDataResult<User>(collection[0]);
            }
            return new ErrorDataResult<User>();
        }

        public DataResult<ObservableCollection<User>> GetAll(Func<User, bool> filter = null)
        {
            S3Base.downloandMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", "warehouses3", null, "user");
            ObservableCollection<User> Collection = new ObservableCollection<User>();
            List<string> Lines = FileManager.Read(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt");
            string[] liste = new string[] { };
            string satir = "";
            for (int i = 0; i < Lines.Count; i++)
            {
                satir = Lines[i];
                if (satir.Trim() != "")
                {
                    liste = satir.Split(",");
                    User user = new User { Id = Convert.ToInt32(liste[0]), CustomerId = Convert.ToInt32(liste[1]), FirstName = liste[2], LastName = liste[3], Email = liste[4], Password = liste[5] };
                    Collection.Add(user);
                }
                
            }
            return filter == null ? new SuccessDataResult<ObservableCollection<User>>(Collection) :
               new SuccessDataResult<ObservableCollection<User>>((ObservableCollection<User>)Collection.Where(filter));
        }

        public Result Update(User entity)
        {
            ObservableCollection<User> collection = GetAll().Data;
            string Data = entity.Id + "," + entity.CustomerId + "," + entity.FirstName + "," + entity.LastName + "," + entity.Email + "," + entity.Password;
            for (int i = 0; i < collection.Count; i++)
            {
                if (entity.Id == collection[i].Id)
                {
                    FileManager.Update(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", i,Data);
                }
            }
            if (S3Base.sendMyFileToS3(@"C:\Users\90542\source\repos\WarehouseProject\s3File\user.txt", "warehouses3", null, "user"))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
