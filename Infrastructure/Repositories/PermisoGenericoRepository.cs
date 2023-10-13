using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PermisoGenericoRepository : GenericRepository<PermisoGenerico>
    {
        public PermisoGenericoRepository(NotiAppJuanJoseContext context) : base(context)
        {
        }
    }
}