using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantContracts.ViewModels;

namespace AutomobilePlantBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoOrdersDate
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersDateViewModel> Orders { get; set; }
    }
}

