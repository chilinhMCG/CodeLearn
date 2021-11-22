using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeLearn.Data
{
    public static class StringUtils
    {
        //slug generator: http://predicatet.blogspot.com/2009/04/improved-c-slug-generator-or-how-to.html
        public static string GenerateSlug(string text, int limit)
        {
            string str = text.RemoveDiacritics().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space
            str = str.Substring(0, str.Length <= limit ? str.Length : limit).Trim(); // cut and trim it
            str = Regex.Replace(str, @"\s", "-"); // hyphens

            return str;
        }

        //remove diacritics: https://stackoverflow.com/questions/37069725/remove-accents-from-a-text-file
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            stringBuilder.Replace("đ", "d").Replace("Đ", "D");

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
