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
            var results = resto.GetAll();
            foreach(var data in results){
                Console.WriteLine(data.restaurantid + " " + data.namarestaurant);
            }
        }
    }

}
