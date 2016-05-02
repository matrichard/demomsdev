using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace DigestTrigger
{
    class Program
    {
        private static DigestInfo[] digests = new DigestInfo[]
        {
            new DigestInfo {UserId = "mathieu.richard5@gmail.com"},
            new DigestInfo {UserId = "potatoe10@yopmail.com"},
            new DigestInfo {UserId = "potatoe101@yopmail.com"},
            new DigestInfo {UserId = "potatoe1011@yopmail.com"},
            new DigestInfo {UserId = "potatoe1012@yopmail.com"},
            new DigestInfo {UserId = "potatoe1013@yopmail.com"},
            new DigestInfo {UserId = "potatoe1014@yopmail.com"},
            new DigestInfo {UserId = "potatoe1015@yopmail.com"},
            new DigestInfo {UserId = "potatoe1016@yopmail.com"},
            new DigestInfo {UserId = "potatoe1017@yopmail.com"},
            new DigestInfo {UserId = "potatoe1018@yopmail.com"},
            new DigestInfo {UserId = "potatoe1019@yopmail.com"},
            new DigestInfo {UserId = "potatoe10101@yopmail.com"},
            new DigestInfo {UserId = "potatoe10101@yopmail.com"},
            new DigestInfo {UserId = "potatoe10103@yopmail.com"},
            new DigestInfo {UserId = "potatoe10104@yopmail.com"},
            new DigestInfo {UserId = "potatoe10105@yopmail.com"},
            new DigestInfo {UserId = "potatoe10106@yopmail.com"},
            new DigestInfo {UserId = "potatoe10107@yopmail.com"},
            new DigestInfo {UserId = "potatoe10108@yopmail.com"},
            new DigestInfo {UserId = "potatoe10109@yopmail.com"},
            new DigestInfo {UserId = "potatoe101001@yopmail.com"},
            new DigestInfo {UserId = "potatoe101003@yopmail.com"},
            new DigestInfo {UserId = "potatoe101002@yopmail.com"},
            new DigestInfo {UserId = "potatoe101004@yopmail.com"},
            new DigestInfo {UserId = "potatoe101005@yopmail.com"},
            new DigestInfo {UserId = "potatoe101006@yopmail.com"},
            new DigestInfo {UserId = "potatoe101007@yopmail.com"},
            new DigestInfo {UserId = "potatoe101008@yopmail.com"},
            new DigestInfo {UserId = "potatoe101009@yopmail.com"},
            new DigestInfo {UserId = "potatoe101000@yopmail.com"},
            new DigestInfo {UserId = "potatoe1010001@yopmail.com"},
            new DigestInfo {UserId = "potatoe1010002@yopmail.com"},
            new DigestInfo {UserId = "potatoe1010003@yopmail.com"}
        }; 
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("process-digest");
            queue.CreateIfNotExists();

            Console.WriteLine("Digest has been triggered and will create message for users");
            foreach (var digestInfo in digests)
            {
                var message = new CloudQueueMessage(JsonConvert.SerializeObject(digestInfo));
                queue.AddMessage(message);
            }
        }
    }

    public class DigestInfo
    {
        public string UserId { get; set; }
    }
}
