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
    public class ModuloMaestroRepository : GenericRepository<ModuloMaestro>, IModuloMaestro
    {
        private readonly NotiAppJuanJoseContext _context;

        public ModuloMaestroRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ModuloMaestro>> GetAllAsync()
        {
            return await _context.ModulosMaestros
                .Include(p => p.RolVsMaestros)     // Se usa así o el 'ThenInclude'.
                .Include(p => p.MaestrosVsSubmodulos)
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<ModuloMaestro> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.ModulosMaestros as IQueryable<ModuloMaestro>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreModulo.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(p => p.RolVsMaestros)  //Se usa asi o con 'ThenInclude' ya que tiene doble relacion
                .Include(p => p.MaestrosVsSubmodulos)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return(totalRegistros, registros);
        }
    }
}