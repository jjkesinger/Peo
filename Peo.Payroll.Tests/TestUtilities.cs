using System.Reflection;

namespace Peo.Payroll.Tests
{
    internal static class TestUtilities
    {
        internal static void SetPrivateProperty<T>(T obj, string propName, object value) 
        { 
            var prop = typeof(T).GetProperty(propName, BindingFlags.Public | BindingFlags.Instance); 
            if (prop != null) 
            { 
                prop.SetValue(obj, value); 
            }
        }
    }
}
