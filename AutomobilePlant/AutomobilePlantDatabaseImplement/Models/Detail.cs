using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomobilePlantDatabaseImplement.Models
{
    /// <summary>
    /// Деталь, требуемая для изготовления автомобиля
    /// </summary>
    public class Detail
    {
        public int Id { get; set; }
        [Required]
        public string DetailName { get; set; }
        [ForeignKey("DetailId")]
        public virtual List<CarDetail> CarDetails { get; set; }
    }
}
