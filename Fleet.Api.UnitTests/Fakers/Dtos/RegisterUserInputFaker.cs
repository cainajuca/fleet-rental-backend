using Bogus;
using Bogus.Extensions.Brazil;
using Fleet.Api._2_Application.UseCases.UserUseCases.Register;

namespace Fleet.Api.UnitTests.Fakers.Dtos;
public class RegisterUserInputFaker : Faker<RegisterUserInput>
{
    public RegisterUserInputFaker()
    {
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Password, f => f.Internet.Password(8, true, @"\w\d"));
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.Name, f => f.Person.FullName);
        RuleFor(x => x.Cnpj, f => f.Company.Cnpj());
        RuleFor(x => x.BirthDate, f => f.Date.Past(30)); // 30 years ago
        RuleFor(x => x.CnhNumber, f => f.Random.AlphaNumeric(10));
        RuleFor(x => x.CnhType, _ => "A");
        RuleFor(x => x.CnhImage, _ => null!); // Set later in tests
    }
}
