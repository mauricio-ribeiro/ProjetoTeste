using ProjetoTeste.Api.Application.DTOs;

namespace ProjetoTeste.Api.Application.DTOs
{
    public class Response<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }
    }

    public class Response : Response<object>
    {
        public Response()
        {
            Dados = null;
        }
    }
}
