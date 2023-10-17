using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BlockchainRepository : GenericRepository<Blockchain>, IBlockchain
    {
        private readonly NotiAppJuanJoseContext _context;

        public BlockchainRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Blockchain>> GetAllAsync()
        {
            return await _context.Blockchains.ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Blockchain> registros)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var query = _context.Blockchains as IQueryable<Blockchain>;
            if(!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.HashGenerado.ToLower().Contains(search));
            }
            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1)* pageSize)
                .Take(pageIndex)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}