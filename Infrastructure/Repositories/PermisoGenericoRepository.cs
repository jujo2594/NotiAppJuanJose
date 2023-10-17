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
    public class PermisoGenericoRepository : GenericRepository<PermisoGenerico>, IPermisoGenerico
    {
        private readonly NotiAppJuanJoseContext _context;

        public PermisoGenericoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<PermisoGenerico>> GetAllAsync()
        {
            return await _context.PermisosGenericos
                .Include(p => p.GenericosVsSubmodulos)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<PermisoGenerico> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.PermisosGenericos as IQueryable<PermisoGenerico>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombrePermiso.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.GenericosVsSubmodulos)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}