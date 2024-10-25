using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Api.Application.DTOs.EnumDto;
using ProjetoTeste.Api.Domain.Enums;

namespace ProjetoTeste.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        // Endpoint para listar todos os estados brasileiros
        [HttpGet("estados")]
        public IActionResult GetEstados()
        {
            var estados = Enum.GetValues(typeof(EstadosBrasilEnum))
                              .Cast<EstadosBrasilEnum>()
                              .Select(e => new EstadosBrasilDto
                              {
                                  Codigo = e.ToString(),
                                  Nome = e.GetDisplayName()
                              });
            return Ok(estados);
        }

        // Endpoint para listar os valores de AtivoEnum
        [HttpGet("ativo")]
        public IActionResult GetAtivoStatus()
        {
            var status = Enum.GetValues(typeof(AtivoEnum))
                             .Cast<AtivoEnum>()
                             .Select(e => new AtivoStatusDto
                             {
                                 Codigo = (int)e,
                                 Nome = e.GetDisplayName()
                             });
            return Ok(status);
        }
    }
}
