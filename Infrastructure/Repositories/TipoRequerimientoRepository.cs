using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TipoRequerimientoRepository : GenericRepository<TipoRequerimiento>, ITipoRequerimiento
    {
        private readonly NotiAppJuanJoseContext _context;

        public TipoRequerimientoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }
    }
}