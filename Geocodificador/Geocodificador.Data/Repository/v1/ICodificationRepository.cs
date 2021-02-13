using Geocodificador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geocodificador.Data.Repository.v1
{
    public interface ICodificationRepository : IRepository<Codification>
    {
        Task<List<Codification>> GetLocalizationRequestsAsync(CancellationToken cancellationToken);

        //Task<Codification> GetLocalizationRequestByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
