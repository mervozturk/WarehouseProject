using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.AWSclouds.S3
{
    public class S3
    {

        const string accessKey = "AKIAUWJ2VAQG2BHHB4PZ";
        const string securityKey = "/PmdQU/y8yuROo7IBi964g34Xh31x8SLKjyn80S6";
        public static AWSCredentials Credentials = new BasicAWSCredentials(accessKey, securityKey);
        public static IAmazonS3 s3Client = new AmazonS3Client(accessKey, securityKey, RegionEndpoint.USEast2);

    }
}
