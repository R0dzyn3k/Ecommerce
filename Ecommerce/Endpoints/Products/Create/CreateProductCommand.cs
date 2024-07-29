namespace Ecommerce.Endpoints.Products.Create;

public class CreateProductCommand
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string? DescriptionShort { get; set; }
    public string Sku { get; set; }
}