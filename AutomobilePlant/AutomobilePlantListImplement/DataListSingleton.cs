using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantListImplement.Models;

namespace AutomobilePlantListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Detail> Details { get; set; }

        public List<Order> Orders { get; set; }

        public List<Car> Cars { get; set; }

        public List<Client> Clients { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        private DataListSingleton()
        {
            Details = new List<Detail>();
            Orders = new List<Order>();
            Cars = new List<Car>();
            Clients = new List<Client>(); 
            Warehouses = new List<Warehouse>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
