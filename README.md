## Core:

Se define a continuacion los Archivos que componen al Core que es la lógica del negocio

#### Entities:

Describe las entidades que configuraran mis tablas de la base de datos , así como la relación entre estás y sus propiedades:

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Auditoria : BaseEntity
    {
        public string NombreUsuario { get; set; }
        public int DesAccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<Blockchain> Blockchains { get; set; }
    }
}
```

#### Interfaces: 

Permite realizar la implementación de mis diferentes entidades, así como la implementación de IUnitOfWork que permite aplicar Singleton y IGenericRepository que me permite definir lo métodos que se aplicaran a mis entidades:

##### IGenericRepository:



```c#
namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T>GetByIdAsync(int id);
        Task<IEnumerable<T>>GetAllAsync();
        IEnumerable<T>Find(Expression<Func<T, bool>> expression); // Metodo que permite realizar busquedas con LinQ

        //Este metodo se define para incrementar la paginación de consultas, Segmenta el tamaño de las consultas y define un parametro de busqueda 
        Task<(int totalRegistros, IEnumerable<T>registros)>GetAllAsync(int pageIndex, int pageSize, String search);
        void Add(T entity);
        void AddRange(IEnumerable<T> entites);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
```

##### IUnitOfWork

```c#
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
```

##### IAuditoria:

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAuditoria : IGenericRepository<Auditoria>
    {
        
    }
}
```



### INFRASTRUCTURE:

Infrastructure se define como la capa o carpeta que permite el acceso a los datos definidos previamente en Core, es por esto que al crear mi proyecto se relaciona la carpeta 'Infrastructure' con 'Core'.



#### DATA:

##### Archivo de Contexto: 

Se definen mis DbSet, que representan mi sesion, establece una conexión con mi base de datos y realiza cambios, consultas en la misma.

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class NotiAppJuanJoseContext : DbContext
{
    public NotiAppJuanJoseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Auditoria> Auditorias { get; set; }
    public DbSet<Blockchain> Blockchains { get; set; }
    public DbSet<EstadoNotificacion> EstadosNotificaciones { get; set; }
    public DbSet<Formato> Formatos { get; set; }
    public DbSet<HiloRespuestaNotificacion> HilosRespuestasNotificaciones { get; set; }
    public DbSet<ModuloNotificacion> ModulosNotificaciones { get; set; }
    public DbSet<Radicado> Radicados { get; set; }
    public DbSet<TipoNotificacion> TiposNotificaciones { get; set; }
    public DbSet<TipoRequerimiento> TiposRequerimientos { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<PermisoGenerico> PermisosGenericos { get; set; }
    public DbSet<RolVsMaestro> RolesVsMaestros { get; set; }
    public DbSet<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }
    public DbSet<ModuloMaestro> ModulosMaestros { get; set; }
    public DbSet<MaestroVsSubmodulo> MaestrosVsSubmodulos { get; set; }
    public DbSet<Submodulo> Submodulos   { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```



#### CONFIGURATION

Carpeta en la defino la configuración de mis entidades para de está manera realizar la migración, defino caracteristicas de mis propiedad asi como la relacion entre mis tablas.

##### AuditoriaConfiguration:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    
    public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("Auditoria");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.NombreUsuario)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.FechaCreacion)
            .HasColumnType("date");

            builder.Property(p => p.FechaModificacion)
            .HasColumnType("date");
        }
            
    }
}
```

#### Repositories:

Defino mis repositorios por cada entidad de mi base de datos en donde defino métodos propios de cada entidad como la paginacion que me ayuda a mantener un orden en mis consultas y adicionalmente defino el GenericRepository que implementa los métodos definidos previamente en mi IGenericRepository.

##### GenericRepository

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly NotiAppJuanJoseContext _context;

        public GenericRepository(NotiAppJuanJoseContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        // El método 'SET<>' Devuelve un 'DbSet<TEntity>' 
        public virtual void AddRange(IEnumerable<T> entites)
        {
            _context.Set<T>().AddRange(entites);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        //Implementacion del método que permite incrementar paginación de consultas en las peticiones. 
        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var totalRegistros = await _context.Set<T>().CountAsync();
            var registros = await _context.Set<T>()
                    .Skip((pageIndex-1)*pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            return (totalRegistros, registros);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
```

#### AuditoriaRepository

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuditoriaRepository : GenericRepository<Auditoria>, IAuditoria
    {
        private readonly NotiAppJuanJoseContext _context;
        public AuditoriaRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Auditoria>> GetAllAsync()
        {
            return await _context.Auditorias
                .Include(p => p.Blockchains)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Auditoria> registros)> GetAllAsync(
            int pageSize,
            int pageIndex,
            string search
        )
        {
            var query = _context.Auditorias as IQueryable<Auditoria>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreUsuario.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.Blockchains)
                .Skip((pageIndex-1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}
```

### UnitOfWork

Permite realizar la implementacion de los metodos definidos en mi IUnitOfWork, e implementar Singlenton, lo que significa que solo realiza la implementacion de una sola clase en mi metodos de extension y me garantiza que solo usare la entidades que requiero, permitiendome un mejor manejo de la memoria.

```c#
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
        public IGenericoVsSubmodulo GenericosVsSubmodulos
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
        public IModuloMaestro ModulosMaestros
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
        public IModuloNotificacion ModulosNotificaciones
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
        public ITipoNotificacion TiposNotificaciones
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
```



### API

API es la carpeta de presentación o la carpeta donde se presenta todo el acceso a la informacion de mi WebApi, en esta carpeta se definen mis controladores, Dtos, metodos de extension, Helpers, y mi AutoMapper.

#### Controllers

Se define y configura el acceso a mis EndPoints

##### AuditoriaController

```c#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuditoriaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuditoriaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuditoriaDto>>> GetAllAsync()
        {
            var auditorias = await _unitOfWork.Auditorias.GetAllAsync();
            return _mapper.Map<List<AuditoriaDto>>(auditorias);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditoriaDto>>GetAllAsync(int id)
        {
            var auditoria = await _unitOfWork.Auditorias.GetByIdAsync(id);
            if(auditoria == null)
            {
                return NotFound();
            }
            return _mapper.Map<AuditoriaDto>(auditoria);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuditoriaDto>> Post(AuditoriaDto auditoriaDto)
        {
            var auditoria = _mapper.Map<Auditoria>(auditoriaDto);
            if(auditoriaDto.FechaCreacion == DateTime.MinValue)
            {
                auditoriaDto.FechaCreacion = DateTime.Now;
                auditoria.FechaModificacion = DateTime.Now;
            }
            if(auditoriaDto.FechaModificacion == DateTime.MinValue)
            {
                auditoriaDto.FechaModificacion = DateTime.Now;
                auditoria.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.Auditorias.Add(auditoria);
            await _unitOfWork.SaveAsync();
            if(auditoria == null)
            {
                return BadRequest();
            }
            auditoriaDto.Id = auditoria.Id;
            return CreatedAtAction(nameof(Post), new{id = auditoriaDto.Id}, auditoriaDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditoriaDto>> Put(int id, [FromBody] AuditoriaDto auditoriaDto)
        {
            if(auditoriaDto.Id == 0){
                auditoriaDto.Id = id;
            }
            if(auditoriaDto == null)
            {
                return NotFound();
            }
            auditoriaDto.Id = id;
            var auditoria = _mapper.Map<Auditoria>(auditoriaDto);
            _unitOfWork.Auditorias.Update(auditoria);
            await _unitOfWork.SaveAsync();
            return auditoriaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var auditoria = await _unitOfWork.Auditorias.GetByIdAsync(id);
            if(auditoria == null)
            {
                return NotFound();
            }
            _unitOfWork.Auditorias.Remove(auditoria);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
```

#### Dtos

Se establece como se visualizara la informacion de mis endpoints, es necesario definir el AutoMapper

##### AuditoriaDto

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class AuditoriaDto : BaseDto
{
    public string NombreUsuario { get; set; }
    public int DesAccion {get; set;} 
}
```



#### Extensions

Se implementan los metodos de extension, como 'CorsPolicy' que configura información de mi Headers, tambien el RateLimit que permite restringir la cantidad de llamados o consultas que pueda realizar el usuarios de mi WebApi evitando el colapso de mi WebApi

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Options;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>{
            options.AddPolicy(
                "CorsPolicy", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
            );
        });
    }

    public static void AddAplicationService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>{
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-Ip";
            options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "10s",
                    Limit = 2
                }
            };
        });
    }
}

```

#### HELPERS

Se define un paginado y parametros para dicho paginado

##### Pager:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;

public class Pager<T> where T : class
{
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Registers { get; private set; }

    public Pager()
    {
        
    }

    public Pager(List<T> registers, int total, int pageIndex, int pageSize, string search)
    {
        Registers = registers;
        Total = total;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Search = search;
    }
    public int TotalPages
    {
        get{return (int)Math.Ceiling(Total/(double)PageSize);}
        set{this.TotalPages = value;}
    }

    public bool HasPreviousPage
    {
        get{return (PageIndex > 1);}
        set{this.HasPreviousPage = value;}
    }

    public bool HasNextPage
    {
        get{return (PageIndex < TotalPages);}
        set{this.HasNextPage = value;}
    }
}

```



##### Params

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;

public class Params
{
    private int _pageSize = 5;
    private const int MaxPageSize = 50;
    private int _pageIndex = 1;
    private string _search;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize: value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0)? 1: value;
    }

    public string Search
    {
        get => _search;
        set => _search = (!String.IsNullOrEmpty(value))? value.ToLower(): "";
    }

}

```



#### PROFILE:

Se define el 'MappingProfile' que me permite realizar un mapeado entre mi entidad y mi Dto

##### MappingProfile

```c#
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
        CreateMap<TipoRequerimiento, TipoNotificacionDto>().ReverseMap();
    }
}

```

