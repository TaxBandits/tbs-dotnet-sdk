using Amazon.S3;
using Amazon.S3.Model;
using System.ComponentModel.DataAnnotations;

namespace Form1099NecSDK.Models.Utilities
{
    public static class Utility
    {
        #region Seperate class for configuration manager
        /// <summary>
        /// ConfigurationManager
        /// </summary>
        static class ConfigurationManager
        {
            public static IConfiguration AppSetting { get; }
            static ConfigurationManager()
            {
                AppSetting = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }
        #endregion

        #region Get App Settings
        /// <summary>
        /// Get the appsetting value from the web.config
        /// </summary>
        /// <param name="appKey">appsettings key</param>
        /// <returns></returns>
        public static string GetAppSettings(string appKey)
        {
            object value = null;
            value = ConfigurationManager.AppSetting.GetSection("AppSettings:" + appKey).Value;
            if (value != null)
            {
                return value.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region Convert to Int
        /// <summary>
        /// Convert to Int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int GetInt(object value)
        {
            int result = 0;
            if (value != null)
            {
                int.TryParse(value.ToString(), out result);
            }
            return result;
        }
        #endregion

        #region Get Enum Display Name values
        public static string GetEnumDisplayName(this Enum value)
        {
            var type = value.GetType();   //will get the enum name which matches the input enum value
            if (type.IsEnum)
            {
                var members = type.GetMember(value.ToString());  // will get the member type
                if (members.Length > 0)
                {
                    var member = members[0];
                    var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);  // gets the custom attribute we described in enum
                    if (attributes.Length > 0)
                    {
                        var attribute = (DisplayAttribute)attributes[0];
                        return attribute.GetName();     // returns the display name string
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        #region Get File Path With Bucket Name using FileName
        private static byte[] GetFilePathWithBucketNameusingFileName(string fileName)
        {

            byte[] toBytes = null;
            AmazonS3Client client = WebStorageConnection();
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
            request.BucketName = GetAppSettings(Constants.BUCKET_NAME);
            request.Key = fileName;
            request.Expires = DateTime.Now.AddHours(1);
            request.Protocol = Protocol.HTTPS;

            string filePath = client.GetPreSignedURL(request);

            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = GetAppSettings(Constants.BUCKET_NAME),
                Key = fileName,
                // Provide encryption information of the object stored in S3.
                ServerSideEncryptionCustomerMethod = ServerSideEncryptionCustomerMethod.AES256,
                ServerSideEncryptionCustomerProvidedKey = GetAppSettings(Constants.BASE_64_KEY),
            };

            // Issue request and remember to dispose of the response
            using (GetObjectResponse response = client.GetObjectAsync(getObjectRequest).Result)
            {
                using (var memoryStream = new MemoryStream())
                {
                    response.ResponseStream.CopyTo(memoryStream);
                    toBytes = memoryStream.ToArray();
                }
            }
            return toBytes;
        }
        #endregion

        #region Get Content Type by Extension
        /// <summary>
        /// Returns the extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        public static string GetContentTypeByExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case "htm":
                case "html":
                case "log":
                    return "text/HTML";
                case "txt":
                    return "text/plain";
                case "docx":
                case "doc":
                    return "application/ms-word";
                case "tiff":
                case "tif":
                    return "image/tiff";
                case "asf":
                    return "video/x-ms-asf";
                case "avi":
                    return "video/avi";
                case "zip":
                    return "application/zip";
                case "xlsx":
                case "xls":
                    return "application/xls";
                case "csv":
                    return "application/vnd.ms-excel";
                case "gif":
                    return "image/gif";
                case "jpg":
                    return "image/jpg";
                case "jpeg":
                    return "image/jpeg";
                case "bmp":
                    return "image/bmp";
                case "png":
                    return "image/png";
                case "pjpeg":
                    return "image/pjpeg";
                case "wav":
                    return "audio/wav";
                case "mp3":
                    return "audio/mpeg3";
                case "mpg":
                case "mpeg":
                    return "video/mpeg";
                case "rtf":
                    return "application/rtf";
                case "asp":
                    return "text/asp";
                case "pdf":
                    return "application/pdf";
                case "fdf":
                    return "application/vnd.fdf";
                case "pps":
                case "pptx":
                case "ppt":
                    return "application/mspowerpoint";
                case "dwg":
                    return "image/vnd.dwg";
                case "msg":
                    return "application/msoutlook";
                case "xml":
                case "sdxl":
                    return "application/xml";
                case "xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
        #endregion

        #region s3 Web Storage Connection

        public static AmazonS3Client WebStorageConnection()
        {
            string accessKey = Utility.GetAppSettings(Constants.AWS_ACCESS_KEY);
            string secretKey = Utility.GetAppSettings(Constants.AWS_SECRET_KEY);

            AmazonS3Config config = new AmazonS3Config();
            config.RegionEndpoint = Amazon.RegionEndpoint.USEast1;
            AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, config);  // connection established

            return client;
        }

        #endregion

        #region GetForm1099NecPdfS3ByFileName
        public static byte[] GetForm1099NecPdfS3ByFileName(string fileName)
        {
            byte[] uploadedFile = null;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                fileName.Replace(@"/ /g", string.Empty);
                string s3Path = GetAppSettings(Constants.AMAZON_S3_PATH);
                fileName = fileName.Replace(s3Path, string.Empty);
                uploadedFile = GetFilePathWithBucketNameusingFileName(fileName);

            }
            return uploadedFile;
        }
        #endregion
    }
}
