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
    public class TipoNotificacionRepository : GenericRepository<TipoNotificacion>, ITipoNotificacion
    {
        private readonly NotiAppJuanJoseContext _context;

        public TipoNotificacionRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<TipoNotificacion>> GetAllAsync()
        {
            return await _context.TiposNotificaciones
            .Include(p => p.Blockchains)
            .Include(p => p.ModulosNotificaciones)
            .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<TipoNotificacion> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search)
        {
            var query = _context.TiposNotificaciones as IQueryable<TipoNotificacion>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreTipo.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.Blockchains)
                .Include(u => u.ModulosNotificaciones)
                .Skip((pageIndex -1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}