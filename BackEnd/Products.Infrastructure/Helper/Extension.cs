using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Products.Infrastructure.Helper
{
    public static class Extension
    {



        public static List<PropertyInfo> GetAttributeBy<TFilter>(this object obj)
        {
            var repositories = new List<PropertyInfo>();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsAssignableFrom(typeof(TFilter)))
                    repositories.Add(property);
            }
            return repositories;
        }

        public static void SetProperty<TObject, TValue>(this TObject obj, PropertyInfo property,
            TValue value)
        {
            if (property != null)
                property.SetValue(obj, value, null);
        }
    }
}
