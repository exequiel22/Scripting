using System;
using System.Collections.Generic;
using System.Text;

namespace Scripting
{
    internal static class Utils
    {
        public static T ConvertOrDefault<T>(this object value) where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
