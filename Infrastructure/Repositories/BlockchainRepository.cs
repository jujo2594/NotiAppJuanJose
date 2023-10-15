using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BlockchainRepository : GenericRepository<Blockchain>, IBlockchain
    {
        private readonly NotiAppJuanJoseContext _context;

        public BlockchainRepository(NotiAppJuanJoseContext context) : base(context)
        {
            _context = context;
        }
    }
}