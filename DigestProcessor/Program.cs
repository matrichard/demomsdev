using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace DigestProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            JobHost host = new JobHost();
            host.RunAndBlock();
        }
    }

    public static class Functions
    {
        private static CloudQueue SendEmailQueue = EnsureQueue();

        private static CloudQueue EnsureQueue()
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("send-email");
            queue.CreateIfNotExists();

            return queue;
        }

        public static void ProcessDigest([QueueTrigger("process-digest")] DigestInfo message)
        {
            Console.WriteLine("Processing digest for {0}", message.UserId);
            var t = Task.Delay(TimeSpan.FromSeconds(1));
            t.Wait();
            var sendEmailMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new SendEmailMessage
            {
                To = message.UserId,
                TemplateId = "digest.json"
            }));

            SendEmailQueue.AddMessage(sendEmailMessage);
        }
    }

    public class DigestInfo
    {
        public string UserId { get; set; }
    }

    public class SendEmailMessage
    {
        public string To { get; set; }

        public string TemplateId { get; set; }
    }
}
