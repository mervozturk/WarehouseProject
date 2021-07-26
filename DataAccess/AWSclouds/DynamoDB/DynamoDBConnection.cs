using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace DataAccess.AWSclouds.DynamoDB
{
    public class DynamoDBConnection
    {
        const string accessKey = "AKIAUWJ2VAQG7VAAYTDQ";
        const string securityKey = "hOHm+i8aw+28INy0IcMVAONZKxmPPiafuwl/tsy+";
        public static AWSCredentials Credentials = new BasicAWSCredentials(accessKey,securityKey);
        public static  AmazonDynamoDBClient client = new AmazonDynamoDBClient(Credentials, RegionEndpoint.USEast2);
    }
}
