using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobilePlantContracts.ViewModels
{
    public class ReportWarehouseDetailsViewModel
    {
        public string WarehouseName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Details { get; set; }
    }
}
