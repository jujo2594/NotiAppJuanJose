using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class FormatoRepository : GenericRepository<Formato>, IFormato
    {
        private readonly NotiAppJuanJoseContext _context;

        public FormatoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }
    }
}