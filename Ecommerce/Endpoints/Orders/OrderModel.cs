using System.Linq.Expressions;
using Ecommerce.Endpoints.Products;
using Ecommerce.Entities;
using Ecommerce.Enums;

namespace Ecommerce.Endpoints.Orders;

public class OrderModel
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime? RealizationAt { get; set; }
    // public int CustomerId { get; set; }
    public string Email { get; set; }
    public bool HasCustomerNote { get; set; }
    public string? CustomerNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    
    public static readonly Expression<Func<Order, OrderModel>> MapFromEntityExpression = order =>
        new OrderModel
        {
            Id = order.Id,
            OrderStatus = order.OrderStatus,
            RealizationAt = order.RealizationAt,
            // CustomerId = order.CustomerId,
            Email = order.Email,
            HasCustomerNote = order.HasCustomerNote,
            CustomerNote = order.CustomerNote,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };

    public static OrderModel FromEntity(Order order)
    {
        return MapFromEntityExpression.Compile().Invoke(order);
    }
}