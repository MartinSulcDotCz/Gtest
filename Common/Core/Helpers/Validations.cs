using Gtest.Common.Core.Enums;
using System.Globalization;

namespace Gtest.Common.Helpers;

public static class Validations
{
	public static bool IsValidLanguageCode(this string code)
	{
		return CultureInfo.GetCultures(CultureTypes.AllCultures)
						  .Any(culture => culture.TwoLetterISOLanguageName.Equals(code, StringComparison.OrdinalIgnoreCase)); // TODO Change it to the list of countries in database, not languages, it is just sample.
	}

	public static bool IsValidLineOfBusiness(this IEnumerable<string> lineOfBusiness)
	{
		if (lineOfBusiness == null || !lineOfBusiness.Any())
			return false;

		foreach (string lob in lineOfBusiness)
		{
			if (!Enum.TryParse<LineOfBusinessEnum>(lob, true, out _))
			{
				return false; // Invalid Line of Business found
			}
		}
		return true; // All Line of Business values are valid
	}
}