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
    public class GenericoVsSubmoduloRepository : GenericRepository<GenericoVsSubmodulo>, IGenericoVsSubmodulo
    {
        private readonly NotiAppJuanJoseContext _context;

        public GenericoVsSubmoduloRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<GenericoVsSubmodulo>> GetAllAsync()
        {
            return await _context.GenericosVsSubmodulos.ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<GenericoVsSubmodulo> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.GenericosVsSubmodulos as IQueryable<GenericoVsSubmodulo>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.IdGenericoFk.ToString().ToLower().Contains(search));
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