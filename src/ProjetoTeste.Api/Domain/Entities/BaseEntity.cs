namespace ProjetoTeste.Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
    }
}
