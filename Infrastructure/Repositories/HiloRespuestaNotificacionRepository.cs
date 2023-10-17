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
    public class HiloRespuestaNotificacionRepository : GenericRepository<HiloRespuestaNotificacion>, IHiloRespuestaNotificacion
    {
        private readonly NotiAppJuanJoseContext _context;

        public HiloRespuestaNotificacionRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<HiloRespuestaNotificacion>> GetAllAsync()
        {
            return await _context.HilosRespuestasNotificaciones
                .Include(p => p.blockchains)
                .Include(p => p.ModulosNotificaciones) //Se usa 'Include' o 'ThenInclude' ??
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<HiloRespuestaNotificacion> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.HilosRespuestasNotificaciones as IQueryable<HiloRespuestaNotificacion>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreTipo.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.blockchains)
                .Include(v => v.ModulosNotificaciones) //Preguntar si está relación es correcta o no ?? 
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }


    }
}