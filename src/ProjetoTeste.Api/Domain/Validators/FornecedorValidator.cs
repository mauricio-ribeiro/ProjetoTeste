using FluentValidation;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Validators.Documentos;

namespace ProjetoTeste.Api.Domain.Validators
{
    public class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(u => u.NomeFantasia)
              .NotEmpty().WithMessage("O campo nome fantasia é obrigatório")
              .Length(3, 100).WithMessage("O campo Nome Fantasia precisa ter entre 3 e 100 caracteres");

            RuleFor(u => u.RazaoSocial)
                .NotEmpty().WithMessage("O campo razão social é obrigatório")
                .Length(3, 150).WithMessage("O campo Razão Social precisa ter entre 3 e 150 caracteres");

            RuleFor(u => u.Documento)
                .NotEmpty().WithMessage("O campo documento é obrigatório")
                .Must(ValidarDocumento).WithMessage("O documento fornecido é inválido.");

            RuleFor(u => u.Email)
               .NotEmpty().WithMessage("O campo e-mail é obrigatório")
               .EmailAddress().WithMessage("O e-mail fornecido não é válido")
               .MaximumLength(150).WithMessage("O e-mail deve ter no máximo 150 caracteres");

            RuleFor(u => u.Ativo)
            .Must(ativo => ativo == false || ativo == true || ativo == Convert.ToBoolean(0) || ativo == Convert.ToBoolean(1))
            .WithMessage("O campo Ativo deve conter um valor válido");
        }

        private bool ValidarDocumento(string documento)
        {
            var documentoNumeros = Utils.ApenasNumeros(documento);
            if (documentoNumeros.Length == CpfValidacao.TamanhoCpf)
            {
                return CpfValidacao.Validar(documento);
            }
            else if (documentoNumeros.Length == CnpjValidacao.TamanhoCnpj)
            {
                return CnpjValidacao.Validar(documento);
            }
            return false; // Retorna falso se não for um CPF ou CNPJ válido
        }
    }
}
