using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace siwar.Domain.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Turn a string into a slug by removing all accents,
    /// special characters, additional spaces, substituting
    /// spaces with hyphens & making it lower-case.
    /// </summary>
    /// <param name="text">The string to turn into a slug.</param>
    public static string Slugify(this string text)
    {
        // Remove all accents and make the string lower case.
        var slug = text.RemoveDiacritics().ToLower();

        // Remove all special characters from the string.
        slug = Regex.Replace(slug, @"[^A-Za-z0-9\s-]", "");

        // Remove all additional spaces in favour of just one.
        slug = Regex.Replace(slug, @"\s+", " ").Trim();

        // Replace all spaces with the hyphen.
        slug = Regex.Replace(slug, @"\s", "-");

        // Return the slug.
        return slug;
    }

    public static string RemoveSpecialCharactersAndSpace(this string text)
    {
        // Remove all accents and make the string lower case.
        text = text.RemoveDiacritics().ToLower();

        // Remove all special characters from the string.
        text = Regex.Replace(text, @"[^A-Za-z0-9\s-]", "");

        // Remove all additional spaces in favour of just one.
        return Regex.Replace(text, @"\s+", "").Trim();
    }

    /// <summary>
    /// Removes all diacritics from the input string.
    /// </summary>
    /// <param name="text">The input string.</param>
    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }
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
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
