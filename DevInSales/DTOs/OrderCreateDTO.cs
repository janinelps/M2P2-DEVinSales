using System.Diagnostics.CodeAnalysis;
using DevInSales.Models;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public int SellerId { get; set; }
        public DateTime Date_Order { get; set; }

    }
}
   