using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotFramework
{
    static class ExtensionMethods
    {
        public static IEnumerable<T> GetCustomAttributesWithInterface<T>(this Type type)
        {
            var attributeType = typeof(T);
            return type.GetCustomAttributes(attributeType, true)
              .Union(type.GetInterfaces().SelectMany(interfaceType =>
                  interfaceType.GetCustomAttributes(attributeType, true)))
              .Cast<T>();
        }
    }
}
