using Bogus;
using DevInSales.Models;

namespace DevInSales.Test.Fixtures
{
    public static class StateFixture
    {
        public static State GeneratePostState() =>
            new Faker<State>()
            .RuleFor(x => x.Name, f => f.Address.State())
            .RuleFor(x => x.Initials, f => f.Address.StateAbbr());

        public static State GenerateState() =>
            new Faker<State>()
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.Name, f => f.Address.State())
            .RuleFor(x => x.Initials, f => f.Address.StateAbbr());
    }
}
