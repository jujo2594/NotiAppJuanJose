using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class GenericoVsSubmoduloDto : BaseDto
{
    public int IdGenericoFk { get; set; }
    public int IdSubmoduloFk { get; set; }
    public int IdRolFk { get; set; }
}
