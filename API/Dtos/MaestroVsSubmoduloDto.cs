using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class MaestroVsSubmoduloDto : BaseDto
{
    public int IdMaestroFk { get; set; }
    public int IdSubmoduloFk { get; set; }
}
