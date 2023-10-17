using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class BlockchainDto : BaseDto
{
    public string HashGenerado { get; set; }
    public int IdTipoNotificacionFk { get; set; }
    public int IdHiloRespuestaFk { get; set; }
    public int IdAuditoriaFk { get; set; }
}
