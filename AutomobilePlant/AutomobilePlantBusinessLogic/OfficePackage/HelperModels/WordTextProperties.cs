using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobilePlantBusinessLogic.OfficePackage.HelperEnums;

namespace AutomobilePlantBusinessLogic.OfficePackage.HelperModels
{
    public class WordTextProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public WordJustificationType JustificationType { get; set; }
    }
}
