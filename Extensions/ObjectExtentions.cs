using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalstreamCommons.Extensions
{
    public static class ObjectExtentions
    {

        public static T1 CopyFrom<T1, T2>(this T1 obj, T2 srcObject)
       where T1 : class
       where T2 : class
        {
            PropertyInfo[] srcFields = srcObject.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            PropertyInfo[] destFields = obj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.CanWrite)
                    dest.SetValue(obj, property.GetValue(srcObject, null), null);
            }

            return obj;
        }
    }
}
