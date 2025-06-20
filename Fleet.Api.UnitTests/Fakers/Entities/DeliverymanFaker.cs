using Bogus;
using Bogus.Extensions.Brazil;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;

namespace Fleet.Api.UnitTests.Fakers.Entities;
public class DeliverymanFaker : Faker<Deliveryman>
{
    public DeliverymanFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Cnpj, f => f.Company.Cnpj());
        RuleFor(x => x.CnhNumber, f => f.Random.AlphaNumeric(10));
        RuleFor(x => x.CnhType, _ => CnhType.A);
    }
}
