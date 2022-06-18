using System.Diagnostics.CodeAnalysis;
using DevInSales.Models;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ProductPatchDTO
    {
        
        public string Name { get; set; }
        
        public decimal Suggested_Price { get; set; }

        public static Product Converter(ProductPatchDTO productModel, int id = 0)
        {
          

            return new Product()
            {
                
                Name = productModel.Name,
                Suggested_Price = productModel.Suggested_Price
            };
        }
    }
}
