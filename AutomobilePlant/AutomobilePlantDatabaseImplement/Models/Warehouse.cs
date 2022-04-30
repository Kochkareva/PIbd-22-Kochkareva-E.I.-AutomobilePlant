using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomobilePlantDatabaseImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        [Required]
        public string WarehouseName { get; set; }
        [Required]
        public string OwnerFullName { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual List<WarehouseDetail> WarehouseDetails { get; set; }
    }
}
