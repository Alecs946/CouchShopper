using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumString : Attribute
    {
        public string Value { get; }

        public EnumString(string value)
        {
            Value = value;
        }
    }
}
