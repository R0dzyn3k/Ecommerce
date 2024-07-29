namespace Ecommerce.Endpoints.Products.Update;

public class UpdateProductCommand
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string DescriptionShort { get; set; }
    public string Sku { get; set; }
}