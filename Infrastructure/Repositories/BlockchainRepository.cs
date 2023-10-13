using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BlockchainRepository : GenericRepository<Blockchain>
    {
        public BlockchainRepository(NotiAppJuanJoseContext context) : base(context)
        {
        }
    }
}