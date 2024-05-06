namespace BuildWise.Extensions
{
    public static class StringExtension
    {
        public static bool IsFill(this string? str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
