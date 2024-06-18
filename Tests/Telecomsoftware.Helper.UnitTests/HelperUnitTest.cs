namespace Telecomsoftware.Helper.UnitTests
{
	public class HelperUnitTest
	{
		[Theory]
		[InlineData("KOESTNER", "KÖSTNER")]
		[InlineData("RUESSWURM", "RÜßWURM")]
		[InlineData("DUERMUELLER", "DÜRMÜLLER")]
		[InlineData("JAEAESKELAEINEN", "JÄÄSKELÄINEN")]
		[InlineData("GROSSSCHAEDL", "GROßSCHÄDL")]
		public void ToUmlaut_ShouldConvertCorrectly(string input, string expected)
		{
			var result = input.ToUmlaut();

			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("KOESTNER", new[] { "KOESTNER", "KÖSTNER" })]
		[InlineData("RUESSWURM", new[] { "RUESSWURM", "RÜSSWURM", "RUEßWURM", "RÜßWURM" })]
		[InlineData("DUERMUELLER", new[] { "DUERMUELLER", "DÜRMUELLER", "DUERMÜLLER" })]
		[InlineData("JAEAESKELAEINEN", new[] { "JAEAESKELAEINEN", "JÄAESKELAEINEN", "JAEÄSKELAEINEN", "JAEAESKELÄINEN" })]
		[InlineData("GROSSSCHAEDL", new[] { "GROSSSCHAEDL", "GROSSSCHÄDL", "GROßSCHAEDL", "GROSßCHAEDL", "GROßSCHÄDL", "GROSßCHÄDL" })]
		public void GenerateAllVariations_ShouldGenerateCorrectVariations(string input, string[] expected)
		{
			var result = input.GenerateAllVariations();

			Assert.Equal(expected.Length, result.Count);
			foreach (var variant in expected)
			{
				Assert.Contains(variant, result);
			}
		}

		[Theory]
		[InlineData("KOESTNER", "SELECT * FROM tbl_phonebook WHERE last_name IN ('KOESTNER', 'KÖSTNER')")]
		[InlineData("RUESSWURM", "SELECT * FROM tbl_phonebook WHERE last_name IN ('RUESSWURM', 'RÜSSWURM', 'RUEßWURM', 'RÜßWURM')")]
		[InlineData("DUERMUELLER", "SELECT * FROM tbl_phonebook WHERE last_name IN ('DUERMUELLER', 'DÜRMUELLER', 'DUERMÜLLER')")]
		[InlineData("JAEAESKELAEINEN", "SELECT * FROM tbl_phonebook WHERE last_name IN ('JAEAESKELAEINEN', 'JÄAESKELAEINEN', 'JAEÄSKELAEINEN', 'JAEAESKELÄINEN')")]
		[InlineData("GROSSSCHAEDL", "SELECT * FROM tbl_phonebook WHERE last_name IN ('GROSSSCHAEDL', 'GROSSSCHÄDL', 'GROßSCHAEDL', 'GROSßCHAEDL', 'GROßSCHÄDL', 'GROSßCHÄDL')")]
		public void GenerateSqlQuery_ShouldGenerateCorrectSql(string input, string expected)
		{
			var result = input.GenerateSqlQuery();

			Assert.Equal(expected, result);
		}
	}
}