using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Rol : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<RolVsMaestro> RolesVsMaestros { get; set; }
        public IEnumerable<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }
    }
}