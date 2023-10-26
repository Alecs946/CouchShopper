using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response
{
    public class ExtendedDropDownModel:DropdownModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string IconName { get; set; }
    }
}
