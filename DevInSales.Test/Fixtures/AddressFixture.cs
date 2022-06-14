using Bogus;
using DevInSales.Models;

namespace DevInSales.Test.Fixtures
{
	public static class AddressFixture
	{
		public static Address GenratePostAddress() =>
		new Faker<Address>()
			   .RuleFor(x => x.City_Id, f => f.Random.Int())
			   .RuleFor(x => x.Street, f => f.Address.StreetAddress())
			   .RuleFor(x => x.Street, f => f.Address.BuildingNumber())
			   .RuleFor(x => x.CEP, f => f.Address.ZipCode());

		public static List<Address> GenrateAddress(int qtdGenerate) =>
		new Faker<Address>()
			   .RuleFor(x => x.City_Id, f => f.Random.Int())
			   .RuleFor(x => x.Street, f => f.Address.StreetAddress())
			   .RuleFor(x => x.Street, f => f.Address.BuildingNumber())
			   .RuleFor(x => x.CEP, f => f.Address.ZipCode())
			   .Generate(qtdGenerate);


	}
}
