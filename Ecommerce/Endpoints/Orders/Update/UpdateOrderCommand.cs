namespace Ecommerce.Endpoints.Orders.Update;

public class UpdateOrderCommand
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public DateTime? RealizationAt { get; set; }
    public string Email { get; set; }
    public bool HasCustomerNote { get; set; }
    public string? CustomerNote { get; set; }
}