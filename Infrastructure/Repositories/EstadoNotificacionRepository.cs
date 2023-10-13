using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EstadoNotificacionRepository : GenericRepository<EstadoNotificacion>
    {
        public EstadoNotificacionRepository(NotiAppJuanJoseContext context) : base(context)
        {
        }
    }
}