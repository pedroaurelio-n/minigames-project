public static class StringExtensions
{
    public static string ToFirstCharUpper (this string s)
    {
        return char.ToUpperInvariant(s[0]) + s.Substring(1);
    }
    
    public static string ToFirstCharLower (this string s)
    {
        return char.ToLowerInvariant(s[0]) + s.Substring(1);
    }
}