namespace AutomobilePlantContracts.BindingModels
{
    /// <summary>
    /// Деталь, требуемая для изготовления автомобиля
    /// </summary>
    public class DetailBindingModel
    {
        public int? Id { get; set; }

        public string DetailName { get; set; }
    }
}
