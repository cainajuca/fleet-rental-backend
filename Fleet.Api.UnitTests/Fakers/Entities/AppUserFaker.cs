using Bogus;
using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;

namespace Fleet.Api.UnitTests.Fakers.Entities;
public class AppUserFaker : Faker<AppUser>
{
    public AppUserFaker()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Username, f => f.Internet.UserName());
        RuleFor(x => x.Email, f => f.Internet.Email());
        RuleFor(x => x.PasswordHash, f => f.Internet.Password(8, true, @"\w\d"));
        RuleFor(x => x.Role, _ => UserRole.Deliveryman);
    }
}
