﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.Enums;
using AutomobilePlantFileImplement.Models;
using System.IO;
using System.Xml.Linq;

namespace AutomobilePlantFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string DetailFileName = "Detail.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string CarFileName = "Car.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        public List<Detail> Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        public FileDataListSingleton()
        {
            Details = LoadDetails();
            Orders = LoadOrders();
            Cars = LoadCars();
            Warehouses = LoadWarehouse();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        public static void SaveFileDataListSingleton()
        {
            instance.SaveDetails();
            instance.SaveOrders();
            instance.SaveCars();
            instance.SaveWarehouses();
        }

        private List<Detail> LoadDetails()
        {
            var list = new List<Detail>();
            if (File.Exists(DetailFileName))
            {
                var xDocument = XDocument.Load(DetailFileName);
                var xElements = xDocument.Root.Elements("Detail").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Detail
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DetailName = elem.Element("DetailName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            // прописать логику
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CarId = Convert.ToInt32(elem.Element("CarId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }

        private List<Car> LoadCars()
        {
            var list = new List<Car>();
            if (File.Exists(CarFileName))
            {
                var xDocument = XDocument.Load(CarFileName);
                var xElements = xDocument.Root.Elements("Car").ToList();
                foreach (var elem in xElements)
                {
                    var carDet = new Dictionary<int, int>();
                    foreach (var detail in
                   elem.Element("CarDetails").Elements("CarDetails").ToList())
                    {
                        carDet.Add(Convert.ToInt32(detail.Element("Key").Value),
                       Convert.ToInt32(detail.Element("Value").Value));
                    }
                    list.Add(new Car
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        CarName = elem.Element("CarName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        CarDetails = carDet
                    });
                }
            }
            return list;
        }

        private List<Warehouse> LoadWarehouse()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                var xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var warehouse in xElements)
                {
                    var warehouseDetails = new Dictionary<int, int>();
                    foreach (var detail in
                        warehouse.Element("WarehouseDetails")
                        .Elements("WarehouseDetail").ToList())
                    {
                        warehouseDetails.Add(Convert.ToInt32(detail.Element("Key").Value),
                        Convert.ToInt32(detail.Element("Value").Value));
                    }

                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(warehouse.Attribute("Id").Value),
                        WarehouseName = warehouse.Element("WarehouseName").Value,
                        OwnerFullName = warehouse.Element("OwnerFullName").Value,
                        DateCreate = Convert.ToDateTime(warehouse.Element("DateCreate").Value),
                        WarehouseDetails = warehouseDetails
                    });
                }
            }
            return list;
        }

        private void SaveDetails()
        {
            if (Details != null)
            {
                var xElement = new XElement("Details");
                foreach (var detail in Details)
                {
                    xElement.Add(new XElement("Detail",
                    new XAttribute("Id", detail.Id),
                    new XElement("DetailName", detail.DetailName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(DetailFileName);
            }
        }

        private void SaveOrders()
        {
            // прописать логику
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                     new XElement("CarId", order.CarId),
                     new XElement("Count", order.Count),
                     new XElement("Sum", order.Sum),
                     new XElement("Status", order.Status),
                     new XElement("DateCreate", order.DateCreate),
                     new XElement("DateImplement", order.DateImplement)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveCars()
        {
            if (Cars != null)
            {
                var xElement = new XElement("Cars");
                foreach (var car in Cars)
                {
                    var detElement = new XElement("CarDetails");
                    foreach (var detail in car.CarDetails)
                    {
                        detElement.Add(new XElement("CarDetails",
                        new XElement("Key", detail.Key),
                        new XElement("Value", detail.Value)));
                    }
                    xElement.Add(new XElement("Car",
                     new XAttribute("Id", car.Id),
                     new XElement("CarName", car.CarName),
                     new XElement("Price", car.Price),
                     detElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(CarFileName);
            }
        }

        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var warehouseDetails = new XElement("WarehouseDetails");
                    foreach (var detail in warehouse.WarehouseDetails)
                    {
                        warehouseDetails.Add(new XElement("WarehouseDetail",
                            new XElement("Key", detail.Key),
                            new XElement("Value", detail.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", warehouse.Id),
                        new XElement("WarehouseName", warehouse.WarehouseName),
                        new XElement("OwnerFullName", warehouse.OwnerFullName),
                        new XElement("DateCreate", warehouse.DateCreate.ToString()),
                        warehouseDetails));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
