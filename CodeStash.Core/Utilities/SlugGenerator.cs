using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeStash.Core.Utilities;

public static partial class SlugGenerator
{
    /// <summary>
    /// Generates a slug from the input string and an ID.
    /// </summary>
    /// <param name="input">The string to slugify</param>
    /// <param name="id">An ID to make the slug unique</param>
    /// <returns></returns>
    public static string GenerateSlug(string input, string id)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var slug = RemoveAccents(input).ToLower();

        slug = RemoveInvalidChars().Replace(slug, ""); // Remove invalid characters
        slug = RemoveMultipleSpaces().Replace(slug, " ").Trim(); // Convert multiple spaces into one space
        slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim(); // Trim to maximum 45 characters
        slug = ReplaceSpacesWithDashes().Replace(slug, "-"); // Replace spaces by dashes

        return $"{slug}-{id.Substring(0, 8)}"; // Append first 8 chars of GUID
    }

    // Generate method to remove accents from a string
    private static string RemoveAccents(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }

    [GeneratedRegex(@"[^A-Za-z0-9\s-]")]
    private static partial Regex RemoveInvalidChars();

    [GeneratedRegex(@"\s+")]
    private static partial Regex RemoveMultipleSpaces();

    [GeneratedRegex(@"\s")]
    private static partial Regex ReplaceSpacesWithDashes();
}
