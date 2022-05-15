using AutomobilePlantContracts.Attributes;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Деталь, требуемая для изготовления автомобиля
    /// </summary>
    public class DetailViewModel
    {
        public int Id { get; set; }

        [Column(title: "Название детали", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DetailName { get; set; }
    }
}
