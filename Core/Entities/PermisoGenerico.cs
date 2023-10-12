using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PermisoGenerico : BaseEntity
    {
        public string NombrePermiso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }
    }
}