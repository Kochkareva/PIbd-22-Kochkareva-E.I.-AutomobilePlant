using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobilePlantContracts.BindingModels
{
    public class AddDetailBindingModel
    {
        public int WarehouseId { get; set; }
        public int DetailId { get; set; }
        public int Count { get; set; }
    }
}
