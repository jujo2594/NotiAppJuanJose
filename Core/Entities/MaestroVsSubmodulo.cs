using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MaestroVsSubmodulo : BaseEntity
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdMaestroFk { get; set; }
        public ModuloMaestro ModulosMaestros { get; set; }
        public int IdSubmoduloFk { get; set; }
        public Submodulo Submodulos { get; set; }
    }
}