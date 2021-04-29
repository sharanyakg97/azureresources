using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azure.Identity;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Samples.Common;
using static ListSubscriptions.Models.ListSubscriptions;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
//using ListSubscriptions.Models.Common.Utililties;
namespace ListSubscriptions.Controllers
{
    public class ListSubscriptionsController : Controller
    {
        // GET: ListSubscriptions
        public ActionResult Index()
        {
            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> ListSubscriptionsAsync( )
        {
           // var credentials = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));
           // AzureEnvironment env = new AzureEnvironment();
            
            DefaultAzureCredential credential = new DefaultAzureCredential();
            await ListSubscriptions.Models.ListSubscriptions.GetSubscriptions(credential);
           
            


            return View();
        }
    }
}
