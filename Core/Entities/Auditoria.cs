using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Auditoria : BaseEntity
    {
        public string NombreUsuario { get; set; }
        public int DesAccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public IEnumerable<Blockchain> Blockchains { get; set; }
    }
}