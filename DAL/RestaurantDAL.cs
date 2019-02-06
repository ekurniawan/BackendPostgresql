using System;
using System.Collections.Generic;
using BO;
using Npgsql;
using Dapper;

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
                //List<Restaurant> lstResto = new List<Restaurant>();
                var strSql = @"select * from restaurants order by namarestaurant";

                var results = conn.Query<Restaurant>(strSql);
                return results;
                /*NpgsqlCommand cmd = new NpgsqlCommand(strSql, conn);
                conn.Open();
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

                return lstResto;*/
            }
        }

        public Restaurant GetByID(int id)
        {
            //Restaurant resto = new Restaurant();
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                var strSql = @"select * from restaurants where restaurantid=@id";
                var param = new { restaurantid = id };
                var result = conn.QuerySingleOrDefault<Restaurant>(strSql, param);

                return result;
                /*NpgsqlCommand cmd = new NpgsqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    resto.restaurantid = Convert.ToInt32(dr[0]);
                    resto.namarestaurant = dr[1].ToString();
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();*/
            }
            //return resto;
        }

        public IEnumerable<Restaurant> GetByNama(string nama)
        {
            using(NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                var strSql = @"select * from restaurants where namarestaurant like @namarestaurant 
                               order by namarestaurant";
                var param = new { namarestaurant = "%" +  nama + "%" };
                return conn.Query<Restaurant>(strSql, param);
            }    
        }

        public void InsertData(Restaurant resto)
        {
            using(NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                var strSql = @"insert into restaurants(namarestaurant) 
                               values(@namarestaurant)";

                NpgsqlCommand cmd = new NpgsqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@namarestaurant", 
                    resto.namarestaurant);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException sqlEx)
                {
                    throw new Exception("Error: "+sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void UpdateData(Restaurant resto)
        {
            using(NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                string strSql = @"update restaurants 
                                  set namarestaurant=@namarestaurant 
                                  where restaurantid=@restaurantid";
                var param = new
                {
                    namarestaurant = resto.namarestaurant,
                    restaurantid = resto.restaurantid
                };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (NpgsqlException sqlEx)
                {
                    throw new Exception($"Kesalahan: {sqlEx.Message}");
                }
            }
        }

        public void DeleteData(int id)
        {
            using(NpgsqlConnection conn = new NpgsqlConnection(GetConnStr()))
            {
                string strSql = @"delete from restaurants where restaurantid=@restaurantid";
                var param = new { restaurantid = id };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (NpgsqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
            }
        }
    }
}
