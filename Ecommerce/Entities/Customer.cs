namespace Ecommerce.Entities;

public class Customer : PersonWithTimeStamp
{
    public string Username { get; set; }
    public string Password { get; set; }
}