using Amazon.DynamoDBv2.Model;
using Core.Results;
using DataAccess.Abstact;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.AWSclouds.DynamoDB
{
    public class DynamoDBUser  /*IAWSclouds<User>*/
    {
        public Result Add(User entity)
        {
            var request = new PutItemRequest
            {
                TableName = "User",
                Item = new Dictionary<string, AttributeValue>()
                {
                     { "ID", new AttributeValue { N = entity.Id.ToString()}},
                     { "CustomerId", new AttributeValue { N = entity.CustomerId.ToString()}},
                     { "Email", new AttributeValue { S = entity.Email}},
                     { "FirstName", new AttributeValue { S = entity.FirstName}},
                     { "LastName", new AttributeValue { S = entity.LastName}},
                     { "Password", new AttributeValue { S = entity.Password}}

                }
            };
            if (DynamoDBConnection.client.PutItemAsync(request).IsCompleted)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public Result Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public DataResult<User> Get(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public DataResult<ObservableCollection<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Result Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
