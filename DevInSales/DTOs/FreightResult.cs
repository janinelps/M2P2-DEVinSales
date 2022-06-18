using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class FreightResult
    {
        public string NameCompany { get; set; }
        public decimal TotalFreight { get; set; }
    }
}
