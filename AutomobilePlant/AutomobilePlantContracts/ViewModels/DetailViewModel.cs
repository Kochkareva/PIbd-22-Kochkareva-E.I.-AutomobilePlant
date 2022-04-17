using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobilePlantContracts.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class DetailViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название компонента")]
        public string DetailName { get; set; }
    }
}
