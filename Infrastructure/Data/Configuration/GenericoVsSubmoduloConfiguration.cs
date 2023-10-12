using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class GenericoVsSubmoduloConfiguration : IEntityTypeConfiguration<GenericoVsSubmodulo>
    {
        public void Configure(EntityTypeBuilder<GenericoVsSubmodulo> builder)
        {
            builder.ToTable("GenericoVsSubmodulo");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.FechaCreacion)
            .HasColumnType("date");

            builder.Property(p => p.FechaModificacion)
            .HasColumnType("date");

            builder.HasOne(p => p.PermisosGenericos)
            .WithMany(p => p.GenericosVsSubmodulos)
            .HasForeignKey(p => p.IdGenericoFk);

            builder.HasOne(p => p.Submodulos)
            .WithMany(p => p.GenericosVsSubmodulos)
            .HasForeignKey(p => p.IdSubmoduloFk);

            builder.HasOne(p => p.Roles)
            .WithMany(p => p.GenericosVsSubmodulos)
            .HasForeignKey(p => p.IdRolFk);
        }
    }
}