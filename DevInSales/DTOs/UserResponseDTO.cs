using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DevInSales.DTOs
{
    /// <summary>
    /// DTO que representa as informações de um usuário
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserResponseDTO
    {
        /// <summary>
        /// O id do usuário
        /// </summary>
        [Display(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// O nome do usuário
        /// </summary>
        [Display(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// O email do usuário
        /// </summary>
        [Display(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// A data de nascimento do usuário
        /// </summary>
        [Display(Name = "birthDate")]
        public DateTime BirthDate { get; set; }
    }
}
