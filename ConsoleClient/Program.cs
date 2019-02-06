using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantDAL resto = new RestaurantDAL();

            try
            {
                var newData = new Restaurant
                {
                    namarestaurant = "Filosofi Kopi"
                };
                resto.InsertData(newData);
                Console.WriteLine("Data berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert data gagal {ex.Message}");
            }

            var results = resto.GetAll();
            foreach(var data in results){
                Console.WriteLine(data.restaurantid + " " + data.namarestaurant);
            }
        }
    }

}
