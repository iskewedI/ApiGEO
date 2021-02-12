using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeoApi.Domain.Entities;


namespace GeoApi.Data.Repository.v1
{
    public interface ILocalizationRequestRepository : IRepository<Localization>
    {
        Task<List<Localization>> GetLocalizationRequestsAsync(CancellationToken cancellationToken);

        Task<Localization> GetLocalizationRequestByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
