using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestDDDApi.Domain.Products
{
    public class ProductID
    {
        public Guid productID { get; set; }
        public ProductID() {}
        public ProductID(Guid guid) 
        {
            this.productID = guid;
        }

        public void UpdateProductID(ProductID productID) 
        {
            this.productID = productID.productID;
        }
    }
}