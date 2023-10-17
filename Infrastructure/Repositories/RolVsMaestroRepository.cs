using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RolVsMaestroRepository : GenericRepository<RolVsMaestro>, IRolVsMaestro
    {
        private readonly NotiAppJuanJoseContext _context;

        public RolVsMaestroRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<RolVsMaestro>> GetAllAsync()
        {
            return await _context.RolesVsMaestros.ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<RolVsMaestro> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search
        )
        {
            var query = _context.RolesVsMaestros as IQueryable<RolVsMaestro>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Id.ToString().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex -1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}