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
    public class MaestroVsSubmoduloRepository : GenericRepository<MaestroVsSubmodulo>, IMaestroVsSubmodulo
    {
        private readonly NotiAppJuanJoseContext _context;

        public MaestroVsSubmoduloRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MaestroVsSubmodulo>> GetAllAsync()
        {
            return await _context.MaestrosVsSubmodulos.ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<MaestroVsSubmodulo> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search
        )
        {
            var query = _context.MaestrosVsSubmodulos as IQueryable<MaestroVsSubmodulo>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.IdMaestroFk.ToString().ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}