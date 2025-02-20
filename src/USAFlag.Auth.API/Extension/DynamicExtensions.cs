using System.Dynamic;
using System.ComponentModel;
using System.Collections.Generic;

namespace USAFlag.AuthAPI.Extension
{
    public static class DynamicExtensions
    {
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }

            return expando as ExpandoObject;
        }
    }
}
