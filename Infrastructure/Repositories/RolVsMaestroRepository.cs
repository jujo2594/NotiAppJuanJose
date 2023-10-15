using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RolVsMaestroRepository : GenericRepository<RolVsMaestro>, IRolVsMaestro
    {
        private readonly NotiAppJuanJoseContext _context;

        public RolVsMaestroRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }
    }
}