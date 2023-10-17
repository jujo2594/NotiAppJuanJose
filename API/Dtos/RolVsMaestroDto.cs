using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class RolVsMaestroDto : BaseDto
{
    public int IdRolFk { get; set; }
    public int IdMaestroFk { get; set; }
}
