using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class AddressDTO
    {
        public string CEP { get; set; }
        public string Street { get; set; }
        public CityStateDTO CityStateDTO { get; set; }
    }
}
