namespace Telecomsoftware.Helper.UnitTests
{
	public class HelperUnitTest
	{
		[Theory]
		[InlineData("KOESTNER", "K�STNER")]
		[InlineData("RUESSWURM", "R��WURM")]
		[InlineData("DUERMUELLER", "D�RM�LLER")]
		[InlineData("JAEAESKELAEINEN", "J��SKEL�INEN")]
		[InlineData("GROSSSCHAEDL", "GRO�SCH�DL")]
		public void ToUmlaut_ShouldConvertCorrectly(string input, string expected)
		{
			var result = input.ToUmlaut();

			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("KOESTNER", new[] { "KOESTNER", "K�STNER" })]
		[InlineData("RUESSWURM", new[] { "RUESSWURM", "R�SSWURM", "RUE�WURM", "R��WURM" })]
		[InlineData("DUERMUELLER", new[] { "DUERMUELLER", "D�RMUELLER", "DUERM�LLER" })]
		[InlineData("JAEAESKELAEINEN", new[] { "JAEAESKELAEINEN", "J�AESKELAEINEN", "JAE�SKELAEINEN", "JAEAESKEL�INEN" })]
		[InlineData("GROSSSCHAEDL", new[] { "GROSSSCHAEDL", "GROSSSCH�DL", "GRO�SCHAEDL", "GROS�CHAEDL", "GRO�SCH�DL", "GROS�CH�DL" })]
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
		[InlineData("KOESTNER", "SELECT * FROM tbl_phonebook WHERE last_name IN ('KOESTNER', 'K�STNER')")]
		[InlineData("RUESSWURM", "SELECT * FROM tbl_phonebook WHERE last_name IN ('RUESSWURM', 'R�SSWURM', 'RUE�WURM', 'R��WURM')")]
		[InlineData("DUERMUELLER", "SELECT * FROM tbl_phonebook WHERE last_name IN ('DUERMUELLER', 'D�RMUELLER', 'DUERM�LLER')")]
		[InlineData("JAEAESKELAEINEN", "SELECT * FROM tbl_phonebook WHERE last_name IN ('JAEAESKELAEINEN', 'J�AESKELAEINEN', 'JAE�SKELAEINEN', 'JAEAESKEL�INEN')")]
		[InlineData("GROSSSCHAEDL", "SELECT * FROM tbl_phonebook WHERE last_name IN ('GROSSSCHAEDL', 'GROSSSCH�DL', 'GRO�SCHAEDL', 'GROS�CHAEDL', 'GRO�SCH�DL', 'GROS�CH�DL')")]
		public void GenerateSqlQuery_ShouldGenerateCorrectSql(string input, string expected)
		{
			var result = input.GenerateSqlQuery();

			Assert.Equal(expected, result);
		}
	}
}