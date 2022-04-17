using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Деталь, требуемая для изготовления автомобиля
    /// </summary>
    public class DetailViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название детали")]
        public string DetailName { get; set; }
    }
}
