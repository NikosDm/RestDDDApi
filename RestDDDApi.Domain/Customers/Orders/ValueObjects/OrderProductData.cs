using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestDDDApi.Domain.Products;

namespace RestDDDApi.Domain.Customers.Orders;

/// <summary>
/// Value object
/// Represents Order Item details 
/// More specifically related Product price and quantity of it
/// </summary>
public class OrderProductData
{
    public Guid productID { get; private set; }
    public int Quantity  { get; private set; }
    public double ProductPrice { get; private set; }

    private OrderProductData() { }

    private OrderProductData(Guid productID, int Quantity, double ProductPrice) 
    {
        this.productID = productID;
        this.Quantity = Quantity;
        this.ProductPrice = ProductPrice;
    }

    private OrderProductData(Guid productID, int Quantity) 
    {
        this.productID = productID;
        this.Quantity = Quantity;
    }

    /// <summary>
    /// Updates Order Item Details 
    /// <param name="productData">Updated Order Item Details</param>
    /// </summary>
    public void UpdateOrderProductData(OrderProductData productData) 
    {
        this.productID = productData.productID;
        this.Quantity = productData.Quantity;
    }

    /// <summary>
    /// Static method that creates an instance of OrderProductData class. 
    /// </summary>
    /// <param name="productID">Product ID</param>
    /// <param name="Quantity">Quantity of the product on Order</param>
    /// <param name="ProductPrice">Price of product</param>
    /// <returns>New instance of OrderProductData Class</returns>
    public static OrderProductData createNewOrderProductData(Guid productID, int Quantity, double ProductPrice) 
    {
        return new OrderProductData(productID, Quantity, ProductPrice);
    }
    
    public static OrderProductData createNewOrderProductData(Guid productID, int Quantity) 
    {
        return new OrderProductData(productID, Quantity);
    }

    /// <returns>Total value of the Order Item</returns>
    public double GetTotalOrderItemAmount() 
    {
        return this.ProductPrice * this.ProductPrice;
    }
}
