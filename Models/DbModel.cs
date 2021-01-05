using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_With_Angular.Models
{
	public class Provider
	{
		public Provider()
		{
			this.Vehicles = new List<Vehicle>();
			this.Drivers = new List<Driver>();
		}
		[Display(Name = "Provider Id")]
		public int ProviderId { get; set; }
		[Required, StringLength(40)]
		[Display(Name = "Provider Name")]
		public string ProviderName { get; set; }
		[Display(Name = "Address")]
		[StringLength(200)]
		public string Address { get; set; }
		[Required, DataType(DataType.EmailAddress), StringLength(50)]
		public string Email { get; set; }
		[DataType(DataType.Url), Url]
		public string Web { get; set; }
		public virtual ICollection<Vehicle> Vehicles { get; set; }
		public virtual ICollection<Driver> Drivers { get; set; }
	}

	public enum VehicleType { Truck=1, Bus, Lorry }

	public class Vehicle
	{
		[Display(Name = "Vehicle Id")]
		public int VehicleID { get; set; }
		[Display(Name = "Vehicle Name"), StringLength(50)]
		public string VehicleName { get; set; }
		[Required]
		[EnumDataType(typeof(VehicleType))]
		[JsonConverter(typeof(StringEnumConverter))]
		public VehicleType Type { get; set; }
		public bool Available { get; set; }
		[Required, ForeignKey("Provider")]
		[Display(Name = "Provider Id")]
		public int ProviderId { get; set; }
		//
		public virtual Provider Provider { get; set; }
	}

	public class Driver
	{
		[Display(Name = "Driver Id")]
		public int DriverId { get; set; }
		[Display(Name = "Driver Name")]
		public string DriverName { get; set; }
		[Display(Name = "Fee")]
		public int Fee { get; set; }
		[Display(Name = "Experiance(Year)")]
		public int Experiance { get; set; }
		[Required, DataType(DataType.EmailAddress), StringLength(50)]
		public string Email { get; set; }
		public bool Available { get; set; }
		[Required, ForeignKey("Provider")]
		[Display(Name = "Provider Id")]
		public int ProviderId { get; set; }
		//
		public virtual Provider Provider { get; set; }
	}
	public class ProviderDbContext : DbContext
	{
		public ProviderDbContext()
		{
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;
		}

		public DbSet<Provider> Providers { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Driver> Drivers { get; set; }
	}
}