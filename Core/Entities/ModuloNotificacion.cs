using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ModuloNotificacion : BaseEntity
    {
        public string AsuntoNotificacion { get; set; }
        public string TextoNotificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdTipoNotificacionFk { get; set; }
        public TipoNotificacion TiposNotificaciones { get; set; }
        public int IdRadicadoFk { get; set; }
        public Radicado Radicados { get; set; }
        public int IdEstadoNotificacionFk { get; set; }
        public EstadoNotificacion EstadosNotificaciones { get; set; }
        public int IdHiloRespuestaFk { get; set; }
        public HiloRespuestaNotificacion HilosRespuestasNotificaciones { get; set; }
        public int IdFormatoFk { get; set; }
        public Formato Formatos { get; set; }
        public int IdRequerimientoFk { get; set; }
        public TipoRequerimiento TiposRequerimientos { get; set; }

    }
}