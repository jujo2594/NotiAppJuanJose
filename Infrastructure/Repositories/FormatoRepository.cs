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
    public class FormatoRepository : GenericRepository<Formato>, IFormato
    {
        private readonly NotiAppJuanJoseContext _context;

        public FormatoRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Formato>> GetAllAsync()
        {
            return await _context.Formatos
                .Include(p => p.ModulosNotificaciones)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Formato> registros)> GetAllAsync(
            int pageIndex, 
            int pageSize, 
            string search
            )
        {
            var query = _context.Formatos as IQueryable<Formato>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p=> p.NombreFormato.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.ModulosNotificaciones)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}