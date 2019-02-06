using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BO;
using DAL;

namespace WebRestful.Controllers
{
    public class RestaurantController : ApiController
    {
        private RestaurantDAL restoDal;
        public RestaurantController()
        {
            restoDal = new RestaurantDAL();
        }

        // GET: api/Restaurant
        public IEnumerable<Restaurant> Get()
        {
            var results = restoDal.GetAll();
            return results;
        }

        [Route("api/Restaurant/GetByName/{id}")]
        [HttpGet]
        public IEnumerable<Restaurant> GetByName(string id)
        {
            var results = restoDal.GetByNama(id);
            return results;
        }

        // GET: api/Restaurant/5
        public Restaurant Get(int id)
        {
            var result = restoDal.GetByID(id);
            return result;
        }

        // POST: api/Restaurant
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Restaurant/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Restaurant/5
        public void Delete(int id)
        {
        }
    }
}
