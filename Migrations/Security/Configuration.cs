namespace Project_With_Angular.Migrations.Security
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Project_With_Angular.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_With_Angular.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Security";
        }

        protected override void Seed(Project_With_Angular.Models.ApplicationDbContext context)
        {
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var user = new ApplicationUser();
			user.UserName = "Admin";
			UserManager.Create(user, "@Open1234");
			base.Seed(context);
		}
    }
}
