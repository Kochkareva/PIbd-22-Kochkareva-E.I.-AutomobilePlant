using System;
using System.Collections.Generic;

namespace AutomobilePlantContracts.BindingModels
{
    /// <summary>
    /// Автомобиль, изготавливаемый на автомобильном заводе
    /// </summary>
    public class CarBindingModel
    {
        public int? Id { get; set; }
        
        public string CarName { get; set; }
        
        public decimal Price { get; set; }
        
        public Dictionary<int, (string, int)> CarDetails { get; set; }
    }
}
