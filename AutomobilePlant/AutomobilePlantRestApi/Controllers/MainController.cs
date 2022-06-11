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
        private readonly int mailsOnPage = 2;
        private int NumOfPages;
        public MainController(IOrderLogic order, ICarLogic car, IMessageInfoLogic messageInfoLogic)
        {
            _order = order;
            _car = car;
            _messageInfoLogic = messageInfoLogic;
            if (mailsOnPage < 1)
            {
                mailsOnPage = 5;
            }
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
        public (List<MessageInfoViewModel>, int) GetMessage(int clientId, int page)
        {
            var fullList = _messageInfoLogic.Read(new MessageInfoBindingModel
            {
                ClientId = clientId,
            });
            NumOfPages = fullList.Count / mailsOnPage;
            if (fullList.Count % mailsOnPage != 0)
            {
                NumOfPages++;
            }
            var list = _messageInfoLogic.Read(new MessageInfoBindingModel 
            { 
                ClientId = clientId, SkipMessage = (page - 1) * mailsOnPage, TakeMessage = mailsOnPage 
            }).ToList();
            return (list.Take(mailsOnPage).ToList(), NumOfPages);
        }
    }
}
