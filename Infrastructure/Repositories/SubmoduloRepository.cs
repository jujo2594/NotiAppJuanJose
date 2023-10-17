using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubmoduloRepository : GenericRepository<Submodulo>, ISubmodulo
    {
        private readonly NotiAppJuanJoseContext _context;

        public SubmoduloRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Submodulo>> GetAllAsync()
        {
            return await _context.Submodulos
                .Include(p => p.MaestrosVsSubmodulos)
                .Include(p => p.GenericosVsSubmodulos)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Submodulo> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search
        )
        {
            var query = _context.Submodulos as IQueryable<Submodulo>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreSubmodulo.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u =>u.MaestrosVsSubmodulos)
                .Include(u =>u.GenericosVsSubmodulos)
                .Skip((pageIndex-1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();

            return (totalRegistros, registros);
        }
                
    }
}