using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace EmailSender
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
        public static void SendEmail([QueueTrigger("send-email")] SendEmailMessage message)
        {
            Console.WriteLine("Sending email to: {0} using templateID: {1}", message.To, message.TemplateId);
        }
    }

    public class SendEmailMessage
    {
        public string To { get; set; }

        public string TemplateId { get; set; }
    }
}
