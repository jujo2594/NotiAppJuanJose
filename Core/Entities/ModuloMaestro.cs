using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ModuloMaestro : BaseEntity
    {
        public string NombreModulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<RolVsMaestro> RolVsMaestros { get; set; }
        public IEnumerable<MaestroVsSubmodulo> MaestrosVsSubmodulos { get; set; }
    }
}