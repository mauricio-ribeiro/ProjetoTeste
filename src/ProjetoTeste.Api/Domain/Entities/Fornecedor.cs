﻿namespace ProjetoTeste.Api.Domain.Entities
{
    public class Fornecedor : BaseEntity
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Documento { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Responsavel { get; set; }
        public string TelFone1 { get; set; }
        public string TelFone2 { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}