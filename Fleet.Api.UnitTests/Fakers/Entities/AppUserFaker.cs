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
        RuleFor(x => x.PasswordHash, f => f.Internet.Password(8, true, @"\w\d"));
        RuleFor(x => x.Name, f => f.Person.FullName);
        RuleFor(x => x.BirthDate, f => f.Date.Past(30)); // 30 years ago
        RuleFor(x => x.Role, _ => UserRole.Deliveryman);
        
        RuleFor(x => x.Deliveryman, (f, au) => 
            new DeliverymanFaker()
                .RuleFor(d => d.AppUserId, au.Id)
                .UseSeed(f.Random.Int())
                .Generate());
    }
}
