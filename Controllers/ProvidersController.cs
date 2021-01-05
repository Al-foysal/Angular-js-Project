using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_With_Angular.Models;

namespace Project_With_Angular.Controllers
{
    public class ProvidersController : ApiController
    {
        private ProviderDbContext db = new ProviderDbContext();

        // GET: api/Providers
        public IQueryable<Provider> GetProviders()
        {
            return db.Providers;
        }

        // GET: api/Providers/5
        [ResponseType(typeof(Provider))]
        public IHttpActionResult GetProvider(int id)
        {
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return NotFound();
            }

            return Ok(provider);
        }

		// PUT: api/Providers/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutProvider(int id, Provider provider)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != provider.ProviderId)
			{
				return BadRequest();
			}

			var ext = db.Providers.Include(x => x.Vehicles).Include(y => y.Drivers).First(x => x.ProviderId == provider.ProviderId);
			ext.ProviderName = provider.ProviderName;
			ext.Address = provider.Address;
			ext.Email = provider.Email;
			ext.Web = provider.Web;			
			if (provider.Vehicles != null && provider.Vehicles.Count > 0)
			{
				var prs = provider.Vehicles.ToArray();
				for (var i = 0; i < prs.Length; i++)
				{
					var temp = ext.Vehicles.FirstOrDefault(c => c.VehicleID == prs[i].VehicleID);
					if (temp != null)
					{
						temp.VehicleName = prs[i].VehicleName;
						temp.Type = prs[i].Type;
						temp.Available = prs[i].Available;
					}
					else
					{
						ext.Vehicles.Add(prs[i]);
					}
				}
				prs = ext.Vehicles.ToArray();
				for (var i = 0; i < prs.Length; i++)
				{
					var temp = provider.Vehicles.FirstOrDefault(x => x.VehicleID == prs[i].VehicleID);
					if (temp == null)
						db.Entry(prs[i]).State = EntityState.Deleted;
				}
			}
			if (provider.Drivers != null && provider.Drivers.Count > 0)
			{
				var srvs = provider.Drivers.ToArray();
				for (var i = 0; i < srvs.Length; i++)
				{
					var temp = ext.Drivers.FirstOrDefault(c => c.DriverId == srvs[i].DriverId);
					if (temp != null)
					{
						temp.DriverName = srvs[i].DriverName;
						temp.Fee = srvs[i].Fee;
						temp.Experiance = srvs[i].Experiance;
						temp.Email = srvs[i].Email;
						temp.Available = srvs[i].Available;
						
					}
					else
					{
						ext.Drivers.Add(srvs[i]);
					}
				}
				srvs = ext.Drivers.ToArray();
				for (var i = 0; i < srvs.Length; i++)
				{
					var temp = provider.Drivers.FirstOrDefault(x => x.DriverId == srvs[i].DriverId);
					if (temp == null)
						db.Entry(srvs[i]).State = EntityState.Deleted;
				}
			}
			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProviderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}
		// POST: api/Providers
		[ResponseType(typeof(Provider))]
        public IHttpActionResult PostProvider(Provider provider)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Providers.Add(provider);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = provider.ProviderId }, provider);
        }

        // DELETE: api/Providers/5
        [ResponseType(typeof(Provider))]
        public IHttpActionResult DeleteProvider(int id)
        {
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return NotFound();
            }

            db.Providers.Remove(provider);
            db.SaveChanges();

            return Ok(provider);
        }
		[Route("custom/Providers")]
		public IQueryable<Provider> GetProviderswithRelation()
		{
			return db.Providers
				.Include("Vehicles")
				.Include("Drivers");
		}
		[Route("custom/Providers/{id}")]
		public IHttpActionResult GetProviderswithRelated(int id)
		{
			return Ok(db.Providers
				.Include("Vehicles")
				.Include("Drivers")
				.FirstOrDefault(x=>x.ProviderId==id));
		}
		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProviderExists(int id)
        {
            return db.Providers.Count(e => e.ProviderId == id) > 0;
        }
    }
}