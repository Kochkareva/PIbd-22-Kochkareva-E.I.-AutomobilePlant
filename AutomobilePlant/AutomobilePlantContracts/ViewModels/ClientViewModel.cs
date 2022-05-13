using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО клиента")]
        public string FullName { get; set; }
        [DisplayName("Логин клиента")]
        public string Login { get; set; }
        [DisplayName("Пароль клиента")]
        public string Password { get; set; }
    }
}
