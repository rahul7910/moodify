using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App2.DataModels;
using Microsoft.WindowsAzure.MobileServices;

namespace App2
{
	public class AzureManager
	{

		private static AzureManager instance;
		private MobileServiceClient client;
		private IMobileServiceTable<EmployeeModel> employeeModelTable;

		private AzureManager()
		{
			this.client = new MobileServiceClient("http://employeeap.azurewebsites.net");
            this.employeeModelTable = this.client.GetTable<EmployeeModel>();
		}

		public MobileServiceClient AzureClient
		{
			get { return client; }
		}

		public static AzureManager AzureManagerInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new AzureManager();
				}

				return instance;
			}
		}
        public async Task<List<EmployeeModel>> GetEmployeeInformation()
		{
            return await this.employeeModelTable.ToListAsync();
		}
	}
}
