using Ecommerce.Common;

namespace Ecommerce.Entities;

public abstract class PersonWithTimeStamp : TimeStampedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}