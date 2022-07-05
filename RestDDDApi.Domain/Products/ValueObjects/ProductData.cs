namespace RestDDDApi.Domain.Products.ValueObjects;

/// <summary>
/// Value object
/// Represents Customer details regarding their address
/// </summary>
public class ProductData
{
    public string Name { get; private set; }
    public Double Price { get; private set; }
    private ProductData() {}
    private ProductData(string Name, double Price) 
    {
        this.Name = Name;
        this.Price = Price;
    }

    /// <summary>
    /// Method for updating the data of a product
    /// More specifically Name and Price
    /// </summary>
    /// <remarks>
    /// Throws exception if at least one the properties is not valid
    /// </remarks>
    /// <param name="productData">Updated product data</param>
    public void UpdateProductData(ProductData productData)
    {
        if (string.IsNullOrWhiteSpace(productData.Name)) throw new Exception("Name of product should not be empty.");
        if (productData.Price <= 0) throw new Exception("Price of Product should be greater than 0");

        this.Name = productData.Name;
        this.Price = productData.Price;
    }

    /// <summary>
    /// Static method for creating ProductData objects 
    /// </summary>
    /// <remarks>
    /// Throws exception if at least one the properties is not valid
    /// </remarks>
    /// <param name="Name">Product Name</param>
    /// <param name="Price">Product price</param>
    /// <returns>ProductData new instance</returns>
    public static ProductData createProductData(string Name, double Price)
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new Exception("Name of product should not be empty.");
        if (Price <= 0) throw new Exception("Price of Product should be greater than 0");

        return new ProductData(Name, Price);
    }
}