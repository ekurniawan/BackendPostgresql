using System;
using System.Collections.Generic;
using BO;
using Npgsql;

namespace DAL
{
    public class RestaurantDAL
    {
        private string GetConnStr()
        {
            return @"Host=23.97.61.111;Port=5432;Username=postgres;
                     Password=actual;Database=SampleDb";
        }

        //get data
        public IEnumerable<Restaurant> GetAll()
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                List<Restaurant> lstResto = new List<Restaurant>();
                var strSql = @"select * from restaurants order by namarestaurant";
                NpgsqlCommand cmd = new NpgsqlCommand(strSql, conn);

                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var newData = new Restaurant
                        {
                            restaurantid = Convert.ToInt32(dr[0]),
                            namarestaurant = dr[1].ToString()
                        };
                        lstResto.Add(newData);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return lstResto;
            }
        }
    }
}
