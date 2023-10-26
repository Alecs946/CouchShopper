using CouchShopper.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);

            if (name == null)
                return null;

            FieldInfo fieldInfo = type.GetField(name);
            EnumString attribute = fieldInfo.GetCustomAttribute<EnumString>(false);

            return attribute?.Value;
        }
    }
}
