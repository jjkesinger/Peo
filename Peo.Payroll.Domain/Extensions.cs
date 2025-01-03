using System.ComponentModel;
using System.Reflection;

namespace Peo.Payroll.Domain
{
    public static class Extensions
    {
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method)
        {
            return await Task.WhenAll(source.Select(async s => await method(s)));
        }
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo field = value.GetType()!.GetField(value.ToString())!; 
            var attribute = field.GetCustomAttribute<DescriptionAttribute>(); 
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
