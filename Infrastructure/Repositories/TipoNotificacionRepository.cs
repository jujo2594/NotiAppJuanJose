using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TipoNotificacionRepository : GenericRepository<TipoNotificacion>
    {
        public TipoNotificacionRepository(NotiAppJuanJoseContext context) : base(context)
        {
        }
    }
}