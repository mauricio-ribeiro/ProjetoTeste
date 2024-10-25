using AutoMapper;
using FluentValidation.Results;
using System.Linq.Expressions;
using ProjetoTeste.Api.Application.DTOs;
using ProjetoTeste.Api.Application.Interfaces;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Responses;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Requests;
using ProjetoTeste.Api.Domain.Interfaces;
using ProjetoTeste.Api.Domain.Validators;

namespace ProjetoTeste.Api.Application.Services
{
    public class FornecedorAppService : IFornecedorAppService, IAppServiceBase<Fornecedor, FornecedorResponse, FornecedorCreateRequest, FornecedorUpdateRequest>
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorAppService(IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        public async Task<Response<FornecedorResponse>> AdicionarAsync(FornecedorCreateRequest request)
        {
            // Verifica se o documento já está registrado no sistema
            var empresaExistente = await _fornecedorService.GetByDocumentoAsync(request.Documento);

            if (empresaExistente != null)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = "Já existe um fornecedor cadastrado com esse documento"
                };
            }

            var fornecedor = _mapper.Map<Fornecedor>(request);

            var validator = new FornecedorValidator();
            ValidationResult result = validator.Validate(fornecedor);

            if (!result.IsValid)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = result.Errors.First().ErrorMessage
                };
            }

            await _fornecedorService.AddAsync(fornecedor);

            var fornecedorResponse = _mapper.Map<FornecedorResponse>(fornecedor);
            return new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor adicionado com sucesso.",
                Dados = fornecedorResponse
            };
        }

        public async Task<Response<FornecedorResponse>> AtualizarAsync(int id, FornecedorUpdateRequest request)
        {
            var fornecedorExistente = await _fornecedorService.GetByIdAsync(id);
            if (fornecedorExistente == null)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = "Fornecdor não encontrado."
                };
            }

            // Verifica duplicidade de Documento
            if (!string.Equals(fornecedorExistente.Documento, request.Documento, StringComparison.OrdinalIgnoreCase))
            {
                var fornecedorComMesmoDocumento = await _fornecedorService.GetByDocumentoAsync(request.Documento);
                if (fornecedorComMesmoDocumento != null && fornecedorComMesmoDocumento.Id != id)
                {
                    return new Response<FornecedorResponse>
                    {
                        Sucesso = false,
                        Mensagem = "Já existe um fornecedor cadastrado com esse documento."
                    };
                }
            }

            _mapper.Map(request, fornecedorExistente);

            var validator = new FornecedorValidator();
            ValidationResult result = validator.Validate(fornecedorExistente);

            if (!result.IsValid)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = result.Errors.First().ErrorMessage
                };
            }

            await _fornecedorService.UpdateAsync(fornecedorExistente);

            var fornecedorResponse = _mapper.Map<FornecedorResponse>(fornecedorExistente);
            return new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor atualizado com sucesso.",
                Dados = fornecedorResponse
            };
        }

        public async Task<Response<FornecedorResponse>> ExcluirAsync(int id)
        {
            var fornecedor = await _fornecedorService.GetByIdAsync(id);
            if (fornecedor == null)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = "Fornecedor não encontrado."
                };
            }

            await _fornecedorService.DeleteAsync(id);
            return new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor excluído com sucesso."
            };
        }

        public async Task<Response<FornecedorResponse>> ObterPorIdAsync(int id)
        {
            var fornecedor = await _fornecedorService.GetByIdAsync(id);
            if (fornecedor == null)
            {
                return new Response<FornecedorResponse>
                {
                    Sucesso = false,
                    Mensagem = "Fornecedor não encontrado."
                };
            }

            var fornecedorResponse = _mapper.Map<FornecedorResponse>(fornecedor);
            return new Response<FornecedorResponse>
            {
                Sucesso = true,
                Dados = fornecedorResponse
            };
        }

        public async Task<Response<IEnumerable<FornecedorResponse>>> ObterTodosAsync()
        {
            var fornecedores = await _fornecedorService.GetAllAsync();
            var fornecedorResponses = _mapper.Map<IEnumerable<FornecedorResponse>>(fornecedores) ?? Enumerable.Empty<FornecedorResponse>();

            return new Response<IEnumerable<FornecedorResponse>>
            {
                Sucesso = true,
                Dados = fornecedorResponses // Isso pode ser uma lista vazia se não houver fornecedores
            };
        }

        public async Task<Response<IEnumerable<FornecedorResponse>>> BuscarAsync(Expression<Func<Fornecedor, bool>> filter = null,
            Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>> orderBy = null, string includeProperties = "")
        {
            var fornecedores = await _fornecedorService.SearchAsync(filter, orderBy, includeProperties);

            if (!fornecedores.Any())
            {
                return new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = false,
                    Mensagem = "Nenhum fornecedor encontrado."
                };
            }

            var fornecedorResponse = _mapper.Map<IEnumerable<FornecedorResponse>>(fornecedores);

            return new Response<IEnumerable<FornecedorResponse>>
            {
                Sucesso = true,
                Dados = fornecedorResponse
            };
        }

    }
}
