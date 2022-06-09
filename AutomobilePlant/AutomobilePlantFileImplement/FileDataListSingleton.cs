using System;
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
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";
        public List<Detail> Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> Messages { get; set; }

        public FileDataListSingleton()
        {
            Details = LoadDetails();
            Orders = LoadOrders();
            Cars = LoadCars();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            Messages = LoadMessages();
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
            instance.SaveClients();
            instance.SaveImplementers();
            instance.SaveMessages();
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

        private List<MessageInfo> LoadMessages()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MessageInfoFileName))
            {
                var xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("Message").ToList();
                int? clientId;
                foreach (var elem in xElements)
                {
                    clientId = null;
                    if (elem.Element("ClientId").Value != "")
                    {
                        clientId = Convert.ToInt32(elem.Element("ClientId").Value);
                    }
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = clientId,
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value
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
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value),
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

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFullName = elem.Element("ClientFullName").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFullName = elem.Element("ImplementerFullName").Value,
                        WorkingTime = Convert.ToInt32(elem.Attribute("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Attribute("PauseTime").Value),
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
                     new XElement("ClientId", order.ClientId),
                     new XElement("ImplementerId", order.ImplementerId),
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
        private void SaveMessages()
        {
            if (Messages != null)
            {
                var xElement = new XElement("Messages");
                foreach (var message in Messages)
                {
                    xElement.Add(new XElement("Message",
                    new XAttribute("MessageId", message.MessageId),
                     new XElement("ClientId", message.ClientId),
                     new XElement("SenderName", message.SenderName),
                     new XElement("DateDelivery", message.DateDelivery),
                     new XElement("Subject", message.Subject),
                     new XElement("Body", message.Body)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(MessageInfoFileName);
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
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFullName", client.ClientFullName),
                    new XElement("Login", client.Login),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFullName", implementer.ImplementerFullName),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
    }
}
