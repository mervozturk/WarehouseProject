using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataAccess.AWSclouds.DynamoDB
{
    public class DynamoDBConnection
    {
        const string accessKey = "AKIAUWJ2VAQG2BHHB4PZ";
        const string securityKey = "/PmdQU/y8yuROo7IBi964g34Xh31x8SLKjyn80S6";
        public static AWSCredentials Credentials = new BasicAWSCredentials(accessKey, securityKey);
        public static AmazonDynamoDBClient client = new AmazonDynamoDBClient(Credentials, RegionEndpoint.USEast2);
    }
}
