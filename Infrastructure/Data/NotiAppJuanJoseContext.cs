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
    