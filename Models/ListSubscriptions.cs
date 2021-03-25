using Azure.Identity;
using Microsoft.Azure.Management.Subscription;
using Azure.Core;
using Microsoft.Rest;
using System.Threading.Tasks;
using System.Diagnostics;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using Azure;
using System;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Samples.Common;
//using Microsoft.Azure.Management.ResourceManager.Fluent.AzureEnvironment;
namespace ListSubscriptions.Models
{
	public class ListSubscriptions
	{
		//var azure = Azure.Configure().WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic).Authenticate(credential).WithDefaultSubscription();

		public static string[] list = new string[20];
		public static string[] vm = new string[15];
		public static string[] stgacct = new string[15];

		public static string SubscriptionID { get; set; }


		public static async Task GetSubscriptions(DefaultAzureCredential credential)
		{
			// Create token request context with scope set to .default
			TokenRequestContext tokenRequestContext = new TokenRequestContext(new[] { "https://management.core.windows.net//.default" });

			AccessToken tokenRequestResult = await credential.GetTokenAsync(tokenRequestContext);


			// Initialize ServiceClientCredential utilizing acquired token
			ServiceClientCredentials serviceClientCreds = new TokenCredentials(tokenRequestResult.Token);


            // ServiceClientCredentials is the abstraction for credentials used by ServiceClients accessing REST services.
            Microsoft.Azure.Management.Subscription.SubscriptionClient subClient = new Microsoft.Azure.Management.Subscription.SubscriptionClient(serviceClientCreds);


			// Get list of subscriptions in tenant
			var listSubscriptions = subClient.Subscriptions.List();
			
			int i = 0;
			// Print out results to the console
			foreach (var sub in listSubscriptions)
			{
				Debug.WriteLine(sub);
				list[i] = sub.DisplayName;
				Debug.WriteLine("array output " + list[i]);
				Debug.WriteLine("Subscription: " + sub.DisplayName);
				i++;
				SubscriptionID = Convert.ToString(sub.SubscriptionId);
				
			}
			

		}
		
		
			
		public static void GetVirtualmachine(IAzure azure, DefaultAzureCredential credential, string subscriptionId)
        {
			ResourcesManagementClient resourceClient = new ResourcesManagementClient(subscriptionId, credential);
			Pageable<ResourceGroup> listResourceGroups = resourceClient.ResourceGroups.List();
			int i = 0;
			foreach (var Group in listResourceGroups)
			{
				//Debug.WriteLine("Resource group: " + Group.Name);
				foreach (var virtualMachine in azure.VirtualMachines.ListByResourceGroup(Group.Name))
				{
					vm[i] = virtualMachine.Name;
					Debug.WriteLine(virtualMachine.Name);
					Utilities.PrintVirtualMachine(virtualMachine);
					i++;
				}
				


			}
		}

		public static void GetStorageAccount(IAzure azure, DefaultAzureCredential credential, string subscriptionId)
		{
			ResourcesManagementClient resourceClient = new ResourcesManagementClient(subscriptionId, credential);
			Pageable<ResourceGroup> listResourceGroups = resourceClient.ResourceGroups.List();
			int i = 0;
			foreach (var Group in listResourceGroups)
			{
				//Debug.WriteLine("Resource group: " + Group.Name);
				
				var storageAccounts = azure.StorageAccounts;

				var accounts = storageAccounts.ListByResourceGroup(Group.Name);

				
				foreach (var account in accounts)
				{
					Debug.WriteLine(account);
					stgacct[i] = account.Name;
					i++;
				}


			}
		}
		public static void GetDatafactory(IAzure azure, DefaultAzureCredential credential, string subscriptionId, Microsoft.Azure.Management.ResourceManager.Fluent.Authentication.AzureCredentials credentials)
		{
			ResourcesManagementClient resourceClient = new ResourcesManagementClient(subscriptionId, credential);
			Pageable<ResourceGroup> listResourceGroups = resourceClient.ResourceGroups.List();
			//var rm = ResourceManager.Configure()
			//.WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
			//.Authenticate(credentials)
			//.WithDefaultSubscription();

			foreach (var Group in listResourceGroups)
			{
				Debug.WriteLine("Resource group: " + Group.Name);
				var resources = resourceClient.Resources.ListByResourceGroup(Group.Name);
				foreach (var rsrce in resources)
                {
					//Debug.WriteLine(rsrce.Type);
					if (String.Compare(rsrce.Type, "Microsoft.DataFactory/factories") ==0) {
						Debug.WriteLine("Resource Name" + rsrce.Name + " Resource Type" + rsrce.Type);
					}
					
                }
				
					
				//var resourcename = resources.ListByResourceGroup(Group.Name);
				//var resources1=rm.GenericResources.List();
				//foreach (var resource in resourcename)
				//{
				//	Debug.WriteLine(resourcename);
					
				//}








			}
		}


	}
}