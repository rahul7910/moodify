using System;
using Newtonsoft.Json;

namespace App2.DataModels
{
		public class EmployeeModel
		{
			[JsonProperty(PropertyName = "Id")]
			public string ID { get; set; }

			[JsonProperty(PropertyName = "Longitude")]
			public float Longitude { get; set; }

			[JsonProperty(PropertyName = "Latitude")]
			public float Latitude { get; set; }
        public string City { get; internal set; }
    }
}


