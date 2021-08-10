using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.AWSclouds.S3
{
    public class S3Base
    {
        public static bool sendMyFileToS3(string FilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            S3 connection = new S3();
            TransferUtility utility = new TransferUtility(S3.s3Client);
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName;   
            }
            else
            {  
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3;
            request.FilePath = FilePath;
            utility.Upload(request); 

            return true; 
        }
        public static  bool downloandMyFileToS3(string FilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            S3 connection = new S3();
            TransferUtility utility = new TransferUtility(S3.s3Client);
            TransferUtilityDownloadRequest request = new TransferUtilityDownloadRequest();
            
            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName;
            }
            else
            {
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3;
            request.FilePath = FilePath;
            utility.Download(request);

            return true;
        }
    }
}
