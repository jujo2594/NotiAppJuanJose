using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NotiAppJuanJoseContext _context;
        private IAuditoria _auditorias;
        private IBlockchain _blockchains;
        private IEstadoNotificacion _estadoNotificaciones;
        private IFormato _formatos;
        private IGenericoVsSubmodulo _genericosVsSubmodulos;
        private IHiloRespuestaNotificacion _hilosRespuestasNotificaciones;
        private IMaestroVsSubmodulo _maestrosVsSubmodulos;
        private IModuloMaestro _modulosMaestros;
        private IModuloNotificacion _modulosNotificaciones;
        private IPermisoGenerico _permisosGenericos;
        private IRadicado _radicados;
        private IRol _roles;
        private IRolVsMaestro _rolesVsMaestros;
        private ISubmodulo _submodulos;
        private ITipoNotificacion _tiposNotificaciones;
        private ITipoRequerimiento _tiposRequerimientos;

        public UnitOfWork(NotiAppJuanJoseContext context)
        {
            _context = context;  
        }
        public IAuditoria Auditorias
        {
            get
            {
                if(_auditorias == null)
                {
                    _auditorias = new AuditoriaRepository(_context);
                }
                return _auditorias;
            }
        }
        public IBlockchain Blockchains
        {
            get
            {
                if(_blockchains == null)
                {
                    _blockchains = new BlockchainRepository(_context);
                }
                return _blockchains;
            }
        }
        public IEstadoNotificacion EstadosNotificaciones
        {
            get
            {
                if(_estadoNotificaciones == null)
                {
                    _estadoNotificaciones = new EstadoNotificacionRepository(_context);
                }
                return _estadoNotificaciones;
            }
        }
        public IFormato Formatos
        {
            get
            {
                if(_formatos == null)
                {
                    _formatos = new FormatoRepository(_context);
                }
                return _formatos;
            }
        }
        public IGenericoVsSubmodulo GenericosVsModulos
        {
            get
            {
                if(_genericosVsSubmodulos == null)
                {
                    _genericosVsSubmodulos = new GenericoVsSubmoduloRepository(_context);
                }
                return _genericosVsSubmodulos;
            }
        }
        public IHiloRespuestaNotificacion HilosRespuestasNotificaciones
        {
            get
            {
                if(_hilosRespuestasNotificaciones == null)
                {
                    _hilosRespuestasNotificaciones = new HiloRespuestaNotificacionRepository(_context);
                }
                return _hilosRespuestasNotificaciones;
            }
        }
        public IMaestroVsSubmodulo MaestrosVsSubmodulos
        {
            get
            {
                if(_maestrosVsSubmodulos == null)
                {
                    _maestrosVsSubmodulos = new MaestroVsSubmoduloRepository(_context);
                }
                return _maestrosVsSubmodulos;
            }
        }
        public IModuloMaestro ModuloMaestro
        {
            get
            {
                if(_modulosMaestros == null)
                {
                    _modulosMaestros = new ModuloMaestroRepository(_context);
                }
                return _modulosMaestros;
            }
        }
        public IModuloNotificacion ModuloNotificacion
        {
            get
            {
                if(_modulosNotificaciones == null)
                {
                    _modulosNotificaciones = new ModuloNotificacionRepository(_context);
                }
                return _modulosNotificaciones;
            }
        }
        public IPermisoGenerico PermisosGenericos
        {
            get
            {
                if(_permisosGenericos == null)
                {
                    _permisosGenericos = new PermisoGenericoRepository(_context);
                }
                return _permisosGenericos;
            }
        }
        public IRadicado Radicados
        {
            get
            {
                if(_radicados == null)
                {
                    _radicados = new RadicadoRepository(_context);
                }
                return _radicados;
            }
        }
        public IRol Roles
        {
            get
            {
                if(_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }
        public IRolVsMaestro RolesVsMaestros
        {
            get
            {
                if(_rolesVsMaestros == null)
                {
                    _rolesVsMaestros = new RolVsMaestroRepository(_context);
                }
                return _rolesVsMaestros;
            }
        }
        public ISubmodulo Submodulos
        {
            get
            {
                if(_submodulos == null)
                {
                    _submodulos = new SubmoduloRepository(_context);
                }
                return _submodulos;
            }
        }
        public ITipoNotificacion TiposModificaciones
        {
            get
            {
                if(_tiposNotificaciones == null)
                {
                    _tiposNotificaciones = new TipoNotificacionRepository(_context);
                }
                return _tiposNotificaciones;
            }
        }
        public ITipoRequerimiento TiposRequerimientos
        {
            get
            {
                if(_tiposRequerimientos == null)
                {
                    _tiposRequerimientos = new TipoRequerimientoRepository(_context);
                }
                return _tiposRequerimientos;
            }
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}