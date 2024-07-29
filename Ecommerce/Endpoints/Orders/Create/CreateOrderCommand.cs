using Ecommerce.Enums;

namespace Ecommerce.Endpoints.Orders.Create;

public class CreateOrderCommand
{ 
    public string Email { get; set; }
    public bool HasCustomerNote { get; set; }
    public string? CustomerNote { get; set; }
}