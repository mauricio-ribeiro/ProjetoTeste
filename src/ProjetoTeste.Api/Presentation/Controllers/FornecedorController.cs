using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Api.Application.DTOs;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Requests;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Responses;
using ProjetoTeste.Api.Application.Interfaces;
using ProjetoTeste.Api.Domain.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Api.Presentation.Controllers
{
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorAppService _fornecedorAppService;

        public FornecedorController(IFornecedorAppService fornecedorAppService)
        {
            _fornecedorAppService = fornecedorAppService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<FornecedorResponse>>> AdicionarFornecedorAsync([FromBody] FornecedorCreateRequest request)
        {
            var response = await _fornecedorAppService.AdicionarAsync(request);
            if (!response.Sucesso)
                return BadRequest(response);

            return CreatedAtAction("ObterFornecedorPorId", new { id = response.Dados.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<FornecedorResponse>>> AtualizarFornecedorAsync(int id, [FromBody] FornecedorUpdateRequest request)
        {
            var response = await _fornecedorAppService.AtualizarAsync(id, request);

            if (!response.Sucesso)
            {
                if (response.Mensagem == "Fornecedor não encontrado.") // Verifica se o erro é "Fornecedor não encontrado"
                    return NotFound(response); // Retorna HTTP 404 NotFound

                return BadRequest(response); // Retorna HTTP 400 BadRequest para outros erros
            }

            return Ok(response); // Retorna HTTP 200 OK se Sucesso for true
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<FornecedorResponse>>> ExcluirFornecedorAsync(int id)
        {
            var response = await _fornecedorAppService.ExcluirAsync(id);
            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}", Name = "ObterFornecedorPorId")]
        public async Task<ActionResult<Response<FornecedorResponse>>> ObterFornecedorPorIdAsync(int id)
        {
            var response = await _fornecedorAppService.ObterPorIdAsync(id);
            if (!response.Sucesso)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<FornecedorResponse>>>> ObterTodosFornecedoresAsync()
        {
            var response = await _fornecedorAppService.ObterTodosAsync();
            return Ok(response);
        }

        [HttpGet("pesquisar")]
        public async Task<ActionResult<Response<IEnumerable<FornecedorResponse>>>> PesquisarFornecedoresAsync(
        [FromQuery] string filtroPesquisa,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null)
        {
            filtroPesquisa = filtroPesquisa?.ToLower();

            // Define datas padrão caso não sejam fornecidas
            var dataInicioFinal = dataInicio ?? DateTime.MinValue;
            var dataFimFinal = (dataFim ?? DateTime.Now).Date.AddDays(1).AddTicks(-1); // Ajuste no final do dia

            // Criação da expressão de filtro, permitindo buscas flexíveis
            Expression<Func<Fornecedor, bool>> filtro = u =>
                (string.IsNullOrEmpty(filtroPesquisa) ||
                u.NomeFantasia.ToLower().Contains(filtroPesquisa) ||
                u.RazaoSocial.ToLower().Contains(filtroPesquisa)) &&
                u.CriadoEm >= dataInicioFinal &&
                u.CriadoEm <= dataFimFinal;

            // Definindo a expressão de ordenação
            Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = q => q.OrderBy(u => u.RazaoSocial);

            // Chama o serviço de aplicação com o filtro
            var response = await _fornecedorAppService.BuscarAsync(filtro, orderBy);

            // Caso a busca tenha falhado, retorne NotFound
            if (!response.Sucesso)
            {
                return NotFound(new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = false,
                    Mensagem = "Nenhum fornecedor encontrado com os critérios informados."
                });
            }

            // Retorna Ok, mesmo que a lista de fornecedores esteja vazia
            return Ok(new Response<IEnumerable<FornecedorResponse>>
            {
                Sucesso = true,
                Dados = response.Dados ?? new List<FornecedorResponse>()
            });
        }

    }
}
