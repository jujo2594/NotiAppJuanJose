using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class HiloRespuestaNotificacion : BaseEntity
    {
        public string NombreTipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<Blockchain> Blockchains { get; set; }
        public IEnumerable<ModuloNotificacion> ModulosNotificaciones { get; set; }
    }
}