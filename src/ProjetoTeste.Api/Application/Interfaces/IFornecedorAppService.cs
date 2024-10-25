using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Requests;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Responses;
using ProjetoTeste.Api.Domain.Entities;

namespace ProjetoTeste.Api.Application.Interfaces
{
    public interface IFornecedorAppService : IAppServiceBase<Fornecedor, FornecedorResponse, FornecedorCreateRequest, FornecedorUpdateRequest>
    {
        
    }
}