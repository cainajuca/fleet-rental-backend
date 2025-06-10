using Bogus;
using Bogus.Extensions.Brazil;
using Fleet.Domain.Entities;

namespace Fleet.Api.UnitTests.Fakers.Entities;
public class DeliverymanFaker : Faker<Deliveryman>
{
    public DeliverymanFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => f.Person.FullName);
        RuleFor(x => x.Cnpj, f => f.Company.Cnpj());
        RuleFor(x => x.BirthDate, f => f.Date.Past(30)); // 30 years ago
        RuleFor(x => x.CnhNumber, f => f.Random.AlphaNumeric(10));
        RuleFor(x => x.CnhType, _ => "A");
        RuleFor(x => x.CnhImageUrl, _ => null!); // Set later in tests
    }
}
