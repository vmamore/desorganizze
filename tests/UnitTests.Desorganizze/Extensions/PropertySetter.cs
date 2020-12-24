using System.Reflection;

namespace UnitTests.Desorganizze.Extensions
{
    public static class PropertySetter
    {
        public static void Set(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();

            var property = type.GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);

            property.SetValue(obj, value);
        }
    }
}
