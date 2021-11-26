
namespace w4sd.Helpers
{
    public static class StaticHelper
    {
        public static object? GetPropertyValue(this object T, string PropName)
        {
            return T.GetType().GetProperty(PropName)?.GetValue(T, null);
        }
    }
}
