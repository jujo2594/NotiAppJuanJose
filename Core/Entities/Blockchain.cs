using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Blockchain : BaseEntity
    {
        public string HashGenerado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdTipoNotificacionFk { get; set; }
        public TipoNotificacion TiposNotificaciones { get; set; }
        public int IdHiloRespuestaFk { get; set; }
        public HiloRespuestaNotificacion HilosRespuestasNotificaciones { get; set; }
        public int IdAuditoriaFk { get; set; }
        public Auditoria Auditorias { get; set; }

    }
}