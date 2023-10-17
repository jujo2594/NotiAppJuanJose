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
    public class AuditoriaRepository : GenericRepository<Auditoria>, IAuditoria
    {
        private readonly NotiAppJuanJoseContext _context;
        public AuditoriaRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Auditoria>> GetAllAsync()
        {
            return await _context.Auditorias
                .Include(p => p.Blockchains)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Auditoria> registros)> GetAllAsync(
            int pageSize,
            int pageIndex,
            string search
        )
        {
            var query = _context.Auditorias as IQueryable<Auditoria>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreUsuario.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(u => u.Blockchains)
                .Skip((pageIndex-1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}