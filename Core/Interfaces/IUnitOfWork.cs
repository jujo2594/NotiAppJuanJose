using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IAuditoria Auditorias {get;}
        IBlockchain Blockchains {get;}
        IEstadoNotificacion EstadosNotificaciones {get;}
        IFormato Formatos {get;}
        IGenericoVsSubmodulo GenericosVsSubmodulos {get;}
        IHiloRespuestaNotificacion HilosRespuestasNotificaciones {get;}
        IMaestroVsSubmodulo MaestrosVsSubmodulos {get;}
        IModuloMaestro ModulosMaestros {get;}
        IModuloNotificacion ModulosNotificaciones {get;}
        IPermisoGenerico PermisosGenericos {get;}
        IRadicado Radicados {get;}
        IRol Roles {get;}
        IRolVsMaestro RolesVsMaestros {get;}
        ISubmodulo Submodulos {get;}
        ITipoNotificacion TiposNotificaciones {get;}
        ITipoRequerimiento TiposRequerimientos {get;}
        Task<int> SaveAsync();
    }
}