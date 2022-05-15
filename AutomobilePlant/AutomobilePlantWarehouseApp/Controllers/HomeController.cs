using AutomobilePlantWarehouseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutomobilePlantContracts.BindingModels;
using AutomobilePlantContracts.ViewModels;
using Microsoft.Extensions.Configuration;

namespace AutomobilePlantWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList"));
        }
       
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                Program.Authorization = password == _configuration["Password"];
                if (!Program.Authorization)
                {
                    throw new Exception("Неверный пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите пароль");
        }

        public IActionResult CreateOrUpdate(int? id)
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Enter");
            }
            if (id == null)
            {
                return View();
            }
            var warehouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList")
                .FirstOrDefault(rec => rec.Id == id);
            if (warehouse == null)
            {
                return View();
            }
            return View(warehouse);
        }
        [HttpPost]
        public void Create([Bind("WarehouseName, OwnerFullName")] WarehouseBindingModel model)
        {
            if (string.IsNullOrEmpty(model.WarehouseName) || string.IsNullOrEmpty(model.OwnerFullName))
            {
                return;
            }
            model.WarehouseDetails = new Dictionary<int, (string, int)>();
            APIClient.PostRequest("api/Warehouse/CreateWarehouse", model);
            Response.Redirect("Index");
           
        }
        [HttpPost]
        public IActionResult Update(int id, [Bind("Id, WarehouseName, OwnerFullName")] WarehouseBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var warehouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList")
                 .FirstOrDefault(rec => rec.Id == id);
            model.WarehouseDetails = warehouse.WarehouseDetails;
            APIClient.PostRequest("api/Warehouse/UpdateWarehouse", model);
            return Redirect("~/Home/Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList")
                .FirstOrDefault(rec => rec.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            APIClient.PostRequest("api/Warehouse/DeleteWarehouse", new WarehouseBindingModel { Id = id });
            return Redirect("~/Home/Index");
        }

        public IActionResult AddDetail()
        {
            if (!Program.Authorization)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList");
            ViewBag.Details = APIClient.GetRequest<List<DetailViewModel>>($"api/Warehouse/GetDetailList");

            return View();
        }

        [HttpPost]
        public IActionResult AddDetail([Bind("WarehouseId, DetailId, Count")] AddDetailBindingModel model)
        {
            if (model.WarehouseId == 0 || model.DetailId == 0 || model.Count <= 0)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList")
                .FirstOrDefault(rec => rec.Id == model.WarehouseId);
            if (warehouse == null)
            {
                return NotFound();
            }

            var detail = APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetDetailList")
                .FirstOrDefault(rec => rec.Id == model.DetailId);
            if (detail == null)
            {
                return NotFound();
            }

            APIClient.PostRequest($"api/Warehouse/FillingWarehouse", model);
            return Redirect("~/Home/AddDetail"); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
