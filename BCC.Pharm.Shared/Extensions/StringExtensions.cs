namespace BCC.Pharm.Shared
{
    public static class StringExtensions
    {
        /// <summary>
        /// Приведение строки к виду, необходимому для сравнения.
        /// </summary>
        /// <param name="source">Искодная строка.</param>
        /// <returns></returns>
        public static string Normilize(this string source) => source?.Trim().ToUpper().Replace(" ", string.Empty);
    }
}