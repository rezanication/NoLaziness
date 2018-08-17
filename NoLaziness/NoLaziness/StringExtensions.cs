namespace NoLaziness
{
    static class StringExtensions
    {
        internal static string[] allowed = { "i", "j", "k", "x", "y" };

        internal static bool IsItTooShort(this string text)
        {
            foreach (var item in allowed)
            {
                if (item == text)
                    return false;
            }

            return text.Length < 3;
        }
    }
}
