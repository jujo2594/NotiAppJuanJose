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
    public class TipoRequerimientoRepository : GenericRepository<TipoRequerimiento>, ITipoRequerimiento
    {
        private readonly NotiAppJuanJoseContext _context;

        public TipoRequerimientoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<TipoRequerimiento>> GetAllAsync()
        {
            return await _context.TiposRequerimientos
            .Include(p => p.ModulosNotificaciones)
            .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<TipoRequerimiento> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search
        )
        {
            var query = _context.TiposRequerimientos as IQueryable<TipoRequerimiento>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.ModulosNotificaciones)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}