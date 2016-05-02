using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace DemoMsDev.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class TaskController : ApiController
    {
        public IHttpActionResult Post()
        {
            // do some hard work and validate task has been completed
            // send email to creator of the task

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("send-email");
            queue.CreateIfNotExists();

            var message = new CloudQueueMessage(JsonConvert.SerializeObject(new SendEmailMessage
            {
                To = "mathieu.richard5@gmail.com",
                TemplateId = "completedtask.json"
            }));

            queue.AddMessage(message);

            return Ok();
        }
    }

    public class SendEmailMessage
    {
        public string To { get; set; }

        public string TemplateId { get; set; }
    }
}