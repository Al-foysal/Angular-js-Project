namespace Project_With_Angular.Migrations.provide
{
	using Project_With_Angular.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_With_Angular.Models.ProviderDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\provide";
        }

        protected override void Seed(Project_With_Angular.Models.ProviderDbContext db)
        {
			Provider p = new Provider { ProviderName = "Baraka Rent Car", Address = "Mirpur,Dhaka", Email = "brc@gmail.com", Web = "https://www.Bcr.com" };
			p.Vehicles.Add(new Vehicle { VehicleName = "TaTa Truck", Type = VehicleType.Truck, Available = false });
			p.Vehicles.Add(new Vehicle { VehicleName = "Toyota Truck", Type = VehicleType.Truck, Available = false });
			p.Drivers.Add(new Driver { DriverName = "Majid", Fee = 800, Experiance = 8, Email = "majid@gmial.com", Available = true });
			db.Providers.Add(p);
			db.SaveChanges();
		}
    }
}
