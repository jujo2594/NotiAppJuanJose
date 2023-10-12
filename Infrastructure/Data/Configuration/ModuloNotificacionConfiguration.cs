using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ModuloNotificacionConfiguration : IEntityTypeConfiguration<ModuloNotificacion>
    {
        public void Configure(EntityTypeBuilder<ModuloNotificacion> builder)
        {
            builder.ToTable("ModuloNotificacion");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.AsuntoNotificacion)
            .IsRequired()
            .HasMaxLength(80);

            builder.Property(p => p.TextoNotificacion)
            .IsRequired()
            .HasMaxLength(2000);

            builder.Property(p => p.FechaCreacion)
            .HasColumnType("date");

            builder.Property(p => p.FechaModificacion)
            .HasColumnType("date");

            builder.HasOne(p => p.TiposNotificaciones)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdTipoNotificacionFk);

            builder.HasOne(p => p.Radicados)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdRadicadoFk);

            builder.HasOne(p => p.EstadosNotificaciones)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdEstadoNotificacionFk);

            builder.HasOne(p => p.HilosRespuestasNotificaciones)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdHiloRespuestaFk);

            builder.HasOne(p => p.Formatos)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdFormatoFk);

            builder.HasOne(p => p.TiposRequerimientos)
            .WithMany(p => p.ModulosNotificaciones)
            .HasForeignKey(p => p.IdRequerimientoFk);
        }
    }
}