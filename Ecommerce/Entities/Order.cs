using Ecommerce.Common;
using Ecommerce.Enums;

namespace Ecommerce.Entities;

public class Order : TimeStampedEntity
{
    public OrderStatus OrderStatus { get; set; }
    
    public DateTime? RealizationAt { get; set; }

    // public int CustomerId { get; set; }

    public string Email { get; set; }

    // public decimal ItemsNet { get; set; }
    // public decimal ItemsTax { get; set; }
    // public decimal ItemsGross { get; set; }
    // public decimal ShippingCost { get; set; }
    // public int ShippingOptionId { get; set; }
    // public string ShippingOptionName { get; set; }
    // public string ShippingOptionDriver { get; set; }
    // public decimal TotalNet { get; set; }
    // public decimal TotalTax { get; set; }
    // public decimal TotalGross { get; set; }
    // public decimal TotalWeight { get; set; }
    public bool HasCustomerNote { get; set; }
    public string? CustomerNote { get; set; }
}