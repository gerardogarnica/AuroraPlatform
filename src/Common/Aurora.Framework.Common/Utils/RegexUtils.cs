using System.Text.RegularExpressions;

namespace Aurora.Framework.Utils
{
    public static partial class RegexUtils
    {
        public static bool IsValidEmail(string email)
        {
            return EmailRegex().IsMatch(email);
        }

        [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        private static partial Regex EmailRegex();
    }
}