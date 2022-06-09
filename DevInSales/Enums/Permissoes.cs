using System.ComponentModel.DataAnnotations;

namespace DevInSales.Enums
{
    public enum Permissoes
    {
        [Display(Name = "Funcionário")]
        Funcionario = 1,

        [Display(Name = "Administrador")]
        Administrador
    }
}
