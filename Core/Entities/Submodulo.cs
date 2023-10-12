using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Submodulo : BaseEntity
    {
        public string NombreSubmodulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<MaestroVsSubmodulo> MaestrosVsSubmodulos { get; set; }
        public IEnumerable<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }

    }
}