using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TipoRequerimiento : BaseEntity
    {
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<ModuloNotificacion> ModulosNotificaciones { get; set; }
    }
}