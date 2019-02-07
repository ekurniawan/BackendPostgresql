using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BO;
using BO.View;
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

        //custom route 
        [Route("api/Restaurant/GetByName/{id}")]
        [HttpGet]
        public IEnumerable<Restaurant> GetByName(string id)
        {
            var results = restoDal.GetByNama(id);
            return results;
        }

        //relasi
        [Route("api/Restaurant/GetRestaurantWithCategory")]
        [HttpGet]
        public IEnumerable<ViewCategoryRestaurant> GetRestaurantWithCategory()
        {
            var results = restoDal.GetRestoWithCategory();
            return results;
        }

        // GET: api/Restaurant/5
        public Restaurant Get(int id)
        {
            var result = restoDal.GetByID(id);
            return result;
        }

        // POST: api/Restaurant
        public IHttpActionResult Post(Restaurant resto)
        {
            try
            {
                restoDal.InsertData(resto);
                return Ok("Data Resto berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest("Kesalahan : " + ex.Message);
            }
        }

        // PUT: api/Restaurant/5
        public IHttpActionResult Put(Restaurant resto)
        {
            try
            {
                restoDal.UpdateData(resto);
                return Ok($"Data resto id: {resto.restaurantid} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ditemukan Kesalahan: {ex.Message}");
            }
        }

        // DELETE: api/Restaurant/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                restoDal.DeleteData(id);
                return Ok($"Data dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
