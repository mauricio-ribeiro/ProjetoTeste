using System.ComponentModel.DataAnnotations;

namespace ProjetoTeste.Api.Domain.Enums
{
    public enum AtivoEnum
    {
        [Display(Name = "Não")]
        Nao = 0,
        [Display(Name = "Sim")]
        Sim = 1
    }
}
