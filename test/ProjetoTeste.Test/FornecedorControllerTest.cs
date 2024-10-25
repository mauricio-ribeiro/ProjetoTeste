using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoTeste.Api.Application.DTOs;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Requests;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Responses;
using ProjetoTeste.Api.Application.Interfaces;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Enums;
using ProjetoTeste.Api.Presentation.Controllers;
using System.Linq.Expressions;

namespace ProjetoTeste.Test
{
    public class FornecedorControllerTest
    {
        private readonly Mock<IFornecedorAppService> _fornecedorAppServiceMock;
        private readonly IMapper _mapper;
        private readonly FornecedorController _controller;

        public FornecedorControllerTest()
        {
            _fornecedorAppServiceMock = new Mock<IFornecedorAppService>();
            var config = new MapperConfiguration(cfg => { /* Configurações do AutoMapper se necessário */ });
            _mapper = config.CreateMapper();
            _controller = new FornecedorController(_fornecedorAppServiceMock.Object);
        }

        [Fact]
        public async Task AdicionarFornecedor_FornecedorValido_RetornaCreated()
        {
            // Arrange
            var request = new FornecedorCreateRequest
            {
                NomeFantasia = "Fornecedor Teste",
                RazaoSocial = "Razão Social Teste",
                Documento = "12345678909", // CPF fictício
                Ativo = AtivoEnum.Sim
            };
            var fornecedorResponse = new FornecedorResponse
            {
                Id = 1,
                NomeFantasia = request.NomeFantasia,
                RazaoSocial = request.RazaoSocial,
                Documento = request.Documento,
                Ativo = true
            };
            _fornecedorAppServiceMock.Setup(x => x.AdicionarAsync(request)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor adicionado com sucesso.",
                Dados = fornecedorResponse
            });

            // Act
            var result = await _controller.AdicionarFornecedorAsync(request);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Equal("Fornecedor adicionado com sucesso.", response.Mensagem);
            Assert.NotNull(response.Dados);
        }

        [Fact]
        public async Task AtualizarFornecedor_FornecedorExistente_RetornaOk()
        {
            // Arrange
            int fornecedorId = 1;
            var request = new FornecedorUpdateRequest
            {
                NomeFantasia = "Fornecedor Atualizado",
                RazaoSocial = "Razão Social Atualizada",
                Documento = "12345678909", // CPF fictício
                Ativo = AtivoEnum.Sim
            };
            var fornecedorResponse = new FornecedorResponse
            {
                Id = fornecedorId,
                NomeFantasia = request.NomeFantasia,
                RazaoSocial = request.RazaoSocial,
                Documento = request.Documento,
                Ativo = true
            };
            _fornecedorAppServiceMock.Setup(x => x.AtualizarAsync(fornecedorId, request)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor atualizado com sucesso.",
                Dados = fornecedorResponse
            });

            // Act
            var result = await _controller.AtualizarFornecedorAsync(fornecedorId, request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Equal("Fornecedor atualizado com sucesso.", response.Mensagem);
            Assert.NotNull(response.Dados);
        }

        [Fact]
        public async Task ExcluirFornecedor_FornecedorExistente_RetornaOk()
        {
            // Arrange
            int fornecedorId = 1;
            _fornecedorAppServiceMock.Setup(x => x.ExcluirAsync(fornecedorId)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = true,
                Mensagem = "Fornecedor excluído com sucesso."
            });

            // Act
            var result = await _controller.ExcluirFornecedorAsync(fornecedorId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Equal("Fornecedor excluído com sucesso.", response.Mensagem);
        }

        [Fact]
        public async Task ObterFornecedorPorId_FornecedorExistente_RetornaOk()
        {
            // Arrange
            int fornecedorId = 1;
            var fornecedorResponse = new FornecedorResponse
            {
                Id = fornecedorId,
                NomeFantasia = "Fornecedor Teste",
                RazaoSocial = "Razão Social Teste",
                Documento = "12345678909", // CPF fictício
                Ativo = true
            };

            _fornecedorAppServiceMock.Setup(x => x.ObterPorIdAsync(fornecedorId)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = true,
                Dados = fornecedorResponse
            });

            // Act
            var result = await _controller.ObterFornecedorPorIdAsync(fornecedorId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.NotNull(response.Dados);
        }

        [Fact]
        public async Task ObterTodosFornecedores_RetornaOk()
        {
            // Arrange
            var fornecedorResponse = new List<FornecedorResponse>
            {
                new FornecedorResponse { Id = 1, NomeFantasia = "Fornecedor 1", RazaoSocial = "Razão Social 1", Documento = "12345678909", Ativo = true },
                new FornecedorResponse { Id = 2, NomeFantasia = "Fornecedor 2", RazaoSocial = "Razão Social 2", Documento = "98765432100", Ativo = true }
            };

            _fornecedorAppServiceMock.Setup(x => x.ObterTodosAsync()).ReturnsAsync(new Response<IEnumerable<FornecedorResponse>>
            {
                Sucesso = true,
                Dados = fornecedorResponse
            });

            // Act
            var result = await _controller.ObterTodosFornecedoresAsync();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<IEnumerable<FornecedorResponse>>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.NotNull(response.Dados);
            Assert.Equal(2, response.Dados.Count());
        }

        [Fact]
        public async Task AdicionarFornecedor_FornecedorInvalido_RetornaBadRequest()
        {
            // Arrange
            var request = new FornecedorCreateRequest
            {
                NomeFantasia = "", // Nome vazio
                RazaoSocial = "Razão Social Teste",
                Documento = "123", // Documento inválido
                Ativo = AtivoEnum.Sim
            };

            _fornecedorAppServiceMock.Setup(x => x.AdicionarAsync(request)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = false,
                Mensagem = "Dados inválidos."
            });

            // Act
            var result = await _controller.AdicionarFornecedorAsync(request);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.False(response.Sucesso);
            Assert.Equal("Dados inválidos.", response.Mensagem);
        }

        [Fact]
        public async Task AtualizarFornecedor_FornecedorNaoExistente_RetornaNotFound()
        {
            // Arrange
            int fornecedorId = 999;
            var request = new FornecedorUpdateRequest
            {
                NomeFantasia = "Fornecedor Atualizada",
                RazaoSocial = "Razão Social Atualizada",
                Documento = "12345678909", // CPF fictício
                Ativo = AtivoEnum.Sim
            };

            _fornecedorAppServiceMock.Setup(x => x.AtualizarAsync(fornecedorId, request)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = false,
                Mensagem = "Fornecedor não encontrado."
            });

            // Act
            var result = await _controller.AtualizarFornecedorAsync(fornecedorId, request);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.False(response.Sucesso);
            Assert.Equal("Fornecedor não encontrado.", response.Mensagem);
        }

        [Fact]
        public async Task ExcluirFornecedor_FornecedorNaoExistente_RetornaNotFound()
        {
            // Arrange
            int fornecedorId = 999;

            _fornecedorAppServiceMock.Setup(x => x.ExcluirAsync(fornecedorId)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = false,
                Mensagem = "Fornecedor não encontrado."
            });

            // Act
            var result = await _controller.ExcluirFornecedorAsync(fornecedorId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.False(response.Sucesso);
            Assert.Equal("Fornecedor não encontrado.", response.Mensagem);
        }

        [Fact]
        public async Task ObterFornecedorPorId_FornecedorNaoExistente_RetornaNotFound()
        {
            // Arrange
            int fornecedorId = 999;

            _fornecedorAppServiceMock.Setup(x => x.ObterPorIdAsync(fornecedorId)).ReturnsAsync(new Response<FornecedorResponse>
            {
                Sucesso = false,
                Mensagem = "Fornecedor não encontrado."
            });

            // Act
            var result = await _controller.ObterFornecedorPorIdAsync(fornecedorId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<Response<FornecedorResponse>>(actionResult.Value);
            Assert.False(response.Sucesso);
            Assert.Equal("Fornecedor não encontrado.", response.Mensagem);
        }


        [Fact]
        public async Task PesquisarFornecedoresAsync_FiltroValido_RetornaFornecedores()
        {
            // Arrange
            var fornecedorResponse = new List<FornecedorResponse>
        {
            new FornecedorResponse { Id = 1, NomeFantasia = "Fornecedor 1", Ativo = true, Documento = "12345678000195" },
            new FornecedorResponse { Id = 2, NomeFantasia = "Fornecedor 2", Ativo = true, Documento = "12345678000196" }
        };

            string filtroPesquisa = "Fornecedor";
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.Now;

            // Configurar o mock para aceitar qualquer filtro e retornar as Fornecedors
            _fornecedorAppServiceMock
                .Setup(x => x.BuscarAsync(It.IsAny<Expression<Func<Fornecedor, bool>>>(),
                                           It.IsAny<Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>>>(),
                                           It.IsAny<string>()))
                .ReturnsAsync(new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = true,
                    Dados = fornecedorResponse
                });

            // Act
            var result = await _controller.PesquisarFornecedoresAsync(filtroPesquisa, dataInicio, dataFim);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<IEnumerable<FornecedorResponse>>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Equal(2, response.Dados.Count());
        }

        [Fact]
        public async Task PesquisarFornecedoresAsync_FiltroVazio_RetornaTodosFornecedores()
        {
            // Arrange
            var fornecedorResponse = new List<FornecedorResponse>
        {
            new FornecedorResponse { Id = 1, NomeFantasia = "Fornecedor 1", Ativo = true, Documento = "12345678000195" },
            new FornecedorResponse { Id = 2, NomeFantasia = "Fornecedor 2", Ativo = true, Documento = "12345678000196" }
        };

            string filtroPesquisa = ""; // Filtro vazio
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.Now;

            // Configurar o mock para retornar todas as Fornecedors
            _fornecedorAppServiceMock
                .Setup(x => x.BuscarAsync(It.IsAny<Expression<Func<Fornecedor, bool>>>(),
                                           It.IsAny<Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>>>(),
                                           It.IsAny<string>()))
                .ReturnsAsync(new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = true,
                    Dados = fornecedorResponse
                });

            // Act
            var result = await _controller.PesquisarFornecedoresAsync(filtroPesquisa, dataInicio, dataFim);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<IEnumerable<FornecedorResponse>>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Equal(2, response.Dados.Count());
        }

        [Fact]
        public async Task PesquisarFornecedoresAsync_FiltroSemResultados_RetornaFornecedoresVazia()
        {
            // Arrange
            string filtroPesquisa = "FornecedorInexistente"; // Filtro que não deve retornar resultados
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.Now;

            // Configurar o mock para retornar uma resposta com Fornecedors vazias
            _fornecedorAppServiceMock
                .Setup(x => x.BuscarAsync(It.IsAny<Expression<Func<Fornecedor, bool>>>(),
                                           It.IsAny<Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>>>(),
                                           It.IsAny<string>()))
                .ReturnsAsync(new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = true,
                    Dados = new List<FornecedorResponse>() // Nenhuma Fornecedor encontrada
                });

            // Act
            var result = await _controller.PesquisarFornecedoresAsync(filtroPesquisa, dataInicio, dataFim);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Response<IEnumerable<FornecedorResponse>>>(actionResult.Value);
            Assert.True(response.Sucesso);
            Assert.Empty(response.Dados); // Verifica que não há dados
        }

        [Fact]
        public async Task PesquisarFornecedoresAsync_FiltroInvalido_RetornaNotFound()
        {
            // Arrange
            string filtroPesquisa = "FiltroInvalido"; // Filtro que não deve retornar resultados
            DateTime dataInicio = DateTime.MinValue;
            DateTime dataFim = DateTime.Now;

            // Configurar o mock para simular falha na busca
            _fornecedorAppServiceMock
                .Setup(x => x.BuscarAsync(It.IsAny<Expression<Func<Fornecedor, bool>>>(),
                                           It.IsAny<Func<IQueryable<Fornecedor>, IOrderedQueryable<Fornecedor>>>(),
                                           It.IsAny<string>()))
                .ReturnsAsync(new Response<IEnumerable<FornecedorResponse>>
                {
                    Sucesso = false, // Simula falha na busca
                    Dados = null // Sem dados
                });

            // Act
            var result = await _controller.PesquisarFornecedoresAsync(filtroPesquisa, dataInicio, dataFim);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var response = Assert.IsType<Response<IEnumerable<FornecedorResponse>>>(actionResult.Value);
            Assert.False(response.Sucesso);
            Assert.Equal("Nenhum fornecedor encontrado com os critérios informados.", response.Mensagem);
        }

    }
}
