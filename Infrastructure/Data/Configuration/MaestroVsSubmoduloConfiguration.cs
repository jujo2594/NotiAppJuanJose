using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class MaestroVsSubmoduloConfiguration : IEntityTypeConfiguration<MaestroVsSubmodulo>
    {
        public void Configure(EntityTypeBuilder<MaestroVsSubmodulo> builder)
        {
            builder.ToTable("MaestroVsSubmodulo");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.FechaCreacion)
            .HasColumnType("date");

            builder.Property(p => p.FechaModificacion)
            .HasColumnType("date");

            builder.HasOne(p => p.ModulosMaestros)
            .WithMany(p => p.MaestrosVsSubmodulos)
            .HasForeignKey(p =>p.IdMaestroFk);

            builder.HasOne(p => p.Submodulos)
            .WithMany(p => p.MaestrosVsSubmodulos)
            .HasForeignKey(p => p.IdSubmoduloFk);
        }
    }
}