using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class BlockchainConfiguration : IEntityTypeConfiguration<Blockchain>
    {
        public void Configure(EntityTypeBuilder<Blockchain> builder)
        {
            builder.ToTable("Blockchain");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.HashGenerado)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(p => p.FechaCreacion)
            .HasColumnType("date");

            builder.Property(p => p.FechaModificacion)
            .HasColumnType("date");

            builder.HasOne(p => p.TiposNotificaciones)
            .WithMany(p => p.Blockchains)
            .HasForeignKey(p => p.IdTipoNotificacionFk);

            builder.HasOne(p => p.HiloRespuestasNotificaciones)
            .WithMany(p => p.blockchains)
            .HasForeignKey(p => p.IdHiloRespuestaFk);

            builder.HasOne(p => p.Auditorias)
            .WithMany(p => p.Blockchains)
            .HasForeignKey(p => p.IdAuditoriaFk);
        }
    }
}