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
using System.Diagnostics;

namespace ListSubscriptions.Controllers
{
    public class VirtualMachineController : Controller
    {
        // GET: VirtualMachine
        public ActionResult Index()
        {
            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> VirtualMachineAsync()
        {
            // var credentials = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));
            // AzureEnvironment env = new AzureEnvironment();
            //var credentias = SdkContext.AzureCredentialsFactory.FromServicePrincipal("bc472b9f-bfd3-4a23-b99a-fbc1e1d35a24", "OaOM_qlh7V17reE_6w0tn_JFr2u82z_6gq","3882b70d-a91e-468c-9928-820358bfbd73",env);
            DefaultAzureCredential credential = new DefaultAzureCredential();
            await ListSubscriptions.Models.ListSubscriptions.GetSubscriptions(credential);
            AzureEnvironment env = AzureEnvironment.FromName("AzureGlobalCloud");
            Debug.WriteLine("environment-->" + env);
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal("bc472b9f-bfd3-4a23-b99a-fbc1e1d35a24", "OaOM_qlh7V17reE_6w0tn_JFr2u82z_6gq", "3882b70d-a91e-468c-9928-820358bfbd73", env);
            //DefaultAzureCredential credential = new DefaultAzureCredential();
            //await ListSubscriptions.Models.ListSubscriptions.GetSubscriptions(credential);
            //var credentiall = SdkContext.AzureCredentialsFactory.
            var azure = Microsoft.Azure.Management.Fluent.Azure.Configure()
                     .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                     .Authenticate(credentials)
                     .WithDefaultSubscription();
            string subscriptionId = ListSubscriptions.Models.ListSubscriptions.SubscriptionID;
            GetVirtualmachine(azure, credential, subscriptionId);


            return View();
        }
    }
}