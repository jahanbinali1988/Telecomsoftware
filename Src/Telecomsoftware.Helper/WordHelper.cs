namespace Telecomsoftware.Helper
{
	public static class WordHelper
	{

		private static readonly Dictionary<string, string> UmlautReplacements = new Dictionary<string, string>
		{
			{ "AE", "Ä" },
			{ "OE", "Ö" },
			{ "UE", "Ü" },
			{ "SS", "ß" }
		};

		/// <summary>
		/// Replace all the Umlauts
		/// </summary>
		/// <param name="givenWord"></param>
		/// <returns></returns>
		public static string ToUmlaut(this string givenWord)
		{
			foreach (var kvp in UmlautReplacements)
			{
				givenWord = givenWord.Replace(kvp.Key, kvp.Value);
			}
			return givenWord;
		}

		/// <summary>
		/// generate all possible words with the given word
		/// </summary>
		/// <param name="givenWord"></param>
		/// <returns></returns>
		public static List<string> GenerateAllVariations(this string givenWord)
		{
			var results = new HashSet<string> { givenWord };
			foreach (var umlautKeys in UmlautReplacements)
			{
				var newWords = new HashSet<string>();
				foreach (var result in results)
				{
					var index = result.IndexOf(umlautKeys.Key);
					while (index != -1)
					{
						//this line of code creates a replaced version of the pattern in the original string.
						var variation = result.Substring(0, index) + umlautKeys.Value + result.Substring(index + umlautKeys.Key.Length);
						newWords.Add(variation);
						//recheck the condition for next replaces
						index = result.IndexOf(umlautKeys.Key, index + 1);
					}
				}
				results.UnionWith(newWords);
			}
			return results.ToList();
		}

		/// <summary>
		/// generate all a sql query for all possibilities of the given word
		/// </summary>
		/// <param name="givenWord"></param>
		/// <returns></returns>
		public static string GenerateSqlQuery(this string givenWord)
		{
			var variations = givenWord.GenerateAllVariations();
			var query = "SELECT * FROM tbl_phonebook WHERE last_name IN ('" + string.Join("', '", variations) + "')";
			return query;
		}

	}
}
