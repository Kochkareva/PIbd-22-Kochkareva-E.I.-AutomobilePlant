using Microsoft.AspNetCore.Mvc;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.BusinessLogicsContracts;
using AutomobilePlantContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace AutomobilePlantRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly IOrderLogic _order;
        private readonly ICarLogic _car;
        private readonly IMessageInfoLogic _messageInfoLogic;
        public MainController(IOrderLogic order, ICarLogic car, IMessageInfoLogic messageInfoLogic)
        {
            _order = order;
            _car = car;
            _messageInfoLogic = messageInfoLogic;
        }
        [HttpGet]
        public List<CarViewModel> GetCarList() => _car.Read(null)?.ToList();
        [HttpGet]
        public CarViewModel GetCar(int carId) => _car.Read(new
       CarBindingModel
        { Id = carId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
        [HttpGet]
        public List<MessageInfoViewModel> GetMessage(int clientId) =>
          _messageInfoLogic.Read(new MessageInfoBindingModel
          {
              ClientId = clientId
          });
    }
}
