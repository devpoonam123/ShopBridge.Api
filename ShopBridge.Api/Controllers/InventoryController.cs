using ShopBridge.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Api.Controllers
{
    public class InventoryController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post(Inventory inventory)
        {
            HttpResponseMessage result;
            if (ModelState.IsValid)
            {
                using (ShopBridgeEntities db = new ShopBridgeEntities())
                {
                    db.Inventories.Add(inventory);
                    db.SaveChanges();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, inventory);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Server failed to save data");
            }
            return result;
        }

        [HttpGet]
        public HttpResponseMessage GetInventoryList()
        {
            HttpResponseMessage result;
            List<Inventory> lstinventorie = new List<Inventory>();
           
                using (ShopBridgeEntities db = new ShopBridgeEntities())
                {
                     lstinventorie= db.Inventories.ToList();
                }
                result = Request.CreateResponse(HttpStatusCode.OK, lstinventorie);
            
            
            return result;
        }

        [HttpDelete()]

        public IHttpActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Not a Inventory valid Id");
            }

            using(ShopBridgeEntities db=new ShopBridgeEntities())
            {
                var Inventory = db.Inventories.SingleOrDefault(m => m.Id.Equals(Id));
                db.Inventories.Remove(Inventory);
                db.SaveChanges();

            }
            return Ok();
        }




    }
}
