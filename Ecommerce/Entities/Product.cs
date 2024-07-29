using Ecommerce.Common;

namespace Ecommerce.Entities;

public class Product : TimeStampedEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string DescriptionShort { get; set; }
    // public string Description { get; set; }
    // public string Ean { get; set; }
    // public string Sku { get; set; }
    // public string Mpn { get; set; }
    // public decimal PackageWeight { get; set; }
    // public decimal PackageWidth { get; set; }
    // public decimal PackageHeight { get; set; }
    // public decimal PackageLength { get; set; }
    // public int TaxGroupId { get; set; }
    // public int ProductCategoryId { get; set; }
}