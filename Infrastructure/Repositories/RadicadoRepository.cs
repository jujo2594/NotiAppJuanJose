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
    public class RadicadoRepository : GenericRepository<Radicado>, IRadicado
    {
        private readonly NotiAppJuanJoseContext _context;
        public RadicadoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Radicado>> GetAllAsync()
        {
            return await _context.Radicados
                .Include(p => p.ModulosNotificaciones)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Radicado> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Radicados as IQueryable<Radicado>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Id.ToString().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(p => p.ModulosNotificaciones)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}