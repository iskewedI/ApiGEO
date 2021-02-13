using Geocodificador.Data.Database.v1;
using Geocodificador.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geocodificador.Data.Repository.v1
{
    public class CodificationRepository : Repository<Codification>, ICodificationRepository
    {
        public CodificationRepository(CodificationContext codificationContext) : base(codificationContext)
        {
        }

        public async Task<List<Codification>> GetLocalizationRequestsAsync(CancellationToken cancellationToken)
        {
            return await CodificationContext.Codification.ToListAsync(cancellationToken);
        }

    }
}
