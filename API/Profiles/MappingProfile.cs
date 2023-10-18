using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Auditoria, AuditoriaDto>().ReverseMap();
        CreateMap<Blockchain, BlockchainDto>().ReverseMap();
        CreateMap<EstadoNotificacion, EstadoNotificacionDto>().ReverseMap();
        CreateMap<Formato, FormatoDto>().ReverseMap();
        CreateMap<GenericoVsSubmodulo,  GenericoVsSubmoduloDto>().ReverseMap();
        CreateMap<HiloRespuestaNotificacion, HiloRespuestaNotificacionDto>().ReverseMap();
        CreateMap<MaestroVsSubmodulo, MaestroVsSubmoduloDto>().ReverseMap();
        CreateMap<ModuloMaestro, ModuloMaestroDto>().ReverseMap();
        CreateMap<ModuloNotificacion, ModuloNotificacionDto>().ReverseMap();
        CreateMap<PermisoGenerico, PermisoGenericoDto>().ReverseMap();
        CreateMap<Radicado, RadicadoDto>().ReverseMap();
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<RolVsMaestro, RolVsMaestroDto>().ReverseMap();
        CreateMap<Submodulo, SubmoduloDto>().ReverseMap();
        CreateMap<TipoNotificacion, TipoNotificacionDto>().ReverseMap();
        CreateMap<TipoRequerimiento, TipoRequerimientoDto>().ReverseMap();
    }
}
