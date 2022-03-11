using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    // <summary>
    /// Автомобиль, изготавливаемый на автомобильном заводе
    /// </summary>
    public class CarViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название автомобиля")]
        public string CarName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> CarDetails { get; set; }
    }
}
