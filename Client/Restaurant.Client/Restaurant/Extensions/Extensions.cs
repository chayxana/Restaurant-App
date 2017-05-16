// ReSharper disable once CheckNamespace
namespace Restaurant
{
    public static class Extensions
    {
        public static string Fmt(this string s, params object[] args)
        {
            return string.Format(s, args);
        }
    }
}