using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    public class WarehouseViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Название склада")]
        public string WarehouseName { get; set; }

        [DisplayName("ФИО ответственного")]
        public string OwnerFullName { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> WarehouseDetails { get; set; }
    }
}
