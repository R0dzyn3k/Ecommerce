using Ecommerce.Enums;

namespace Ecommerce.Entities;

public class Administrator : PersonWithTimeStamp
{
    public string Username { get; set; }
    public string Password { get; set; }
    public AdministratorRole Role { get; set; }
}