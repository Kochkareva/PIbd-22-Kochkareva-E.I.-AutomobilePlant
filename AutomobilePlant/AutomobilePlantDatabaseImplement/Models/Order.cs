using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace AutomobilePlantDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public int ClientId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Car Car { get; set; }
        public virtual Client Client { get; set; }
    }
}
