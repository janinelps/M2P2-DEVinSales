using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    [ExcludeFromCodeCoverage]
    public class UserLoginDTO
    {
        public string Password { get; set; }
        
        public string Email { get; set; }
    }
}
