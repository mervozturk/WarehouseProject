using Amazon.DynamoDBv2.Model;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
namespace DataAccess.AWSclouds.DynamoDB
{
    public class DynamoDBProduct : IAWSclouds<Product>
    {
        public Result Add(Product entity)
        {
            var request = new PutItemRequest
            {
                TableName="Product",
                Item = new Dictionary<string, AttributeValue>()
                {
                    { "Id", new AttributeValue { N = entity.Id.ToString()}},
                    { "CategoryID", new AttributeValue { N = entity.CategoryId.ToString()}},
                    { "WarehouseId", new AttributeValue { N = entity.WarehouseID.ToString()}},
                    { "ProductName", new AttributeValue { S = entity.ProductName}},
                    { "UnitPrice", new AttributeValue { N = entity.UnitPrice.ToString()}},
                    { "UnitsInStok", new AttributeValue { N = entity.UnitsInStock.ToString()}},
                    { "Description", new AttributeValue { S = entity.Description}},
                }
            };
            if (DynamoDBConnection.client.PutItemAsync(request).IsCompleted)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public DataResult<Product> Get(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public DataResult<ObservableCollection<Product>> GetAll(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public Result Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
