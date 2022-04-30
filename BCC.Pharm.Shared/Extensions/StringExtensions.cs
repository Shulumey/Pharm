namespace BCC.Pharm.Shared
{
    public static class StringExtensions
    {
        public static string Normilize(this string source) => source?.Trim().ToUpper().Replace(" ", string.Empty);
    }
}