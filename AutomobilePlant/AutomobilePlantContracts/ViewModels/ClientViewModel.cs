using AutomobilePlantContracts.Attributes;
using System.ComponentModel;

namespace AutomobilePlantContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FullName { get; set; }

        [Column(title: "Логин клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Login { get; set; }

        [Column(title: "Пароль клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Password { get; set; }
    }
}
