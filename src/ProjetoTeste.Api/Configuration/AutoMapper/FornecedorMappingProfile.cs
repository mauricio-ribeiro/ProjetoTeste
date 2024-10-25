using AutoMapper;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Requests;
using ProjetoTeste.Api.Application.DTOs.FornecedorDto.Responses;
using ProjetoTeste.Api.Domain.Entities;
using ProjetoTeste.Api.Domain.Enums;

namespace ProjetoTeste.Api.Configuration.AutoMapper
{
    public class FornecedorMappingProfile : Profile
    {
        public FornecedorMappingProfile()
        {
            CreateMap<FornecedorCreateRequest, Fornecedor>()
                 .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo == AtivoEnum.Sim));

            CreateMap<FornecedorUpdateRequest, Fornecedor>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo == AtivoEnum.Sim));

            CreateMap<Fornecedor, FornecedorResponse>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.AtivoEnum, opt => opt.MapFrom(src => src.Ativo ? AtivoEnum.Sim : AtivoEnum.Nao))
                .ForMember(dest => dest.AtivoDesc, opt => opt.MapFrom(src => (src.Ativo ? AtivoEnum.Sim : AtivoEnum.Nao).GetDisplayName()));
        }
    }
}
