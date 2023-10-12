using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GenericoVsSubmodulo : BaseEntity
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdGenericoFk { get; set; }
        public PermisoGenerico PermisosGenericos { get; set; }
        public int IdSubmoduloFk { get; set; }
        public Submodulo Submodulos { get; set; }
        public int IdRolFk { get; set; }
        public Rol Roles { get; set; }
    }
}