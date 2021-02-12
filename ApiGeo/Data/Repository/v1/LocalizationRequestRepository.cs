using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GeoApi.Data.Database.v1;
using GeoApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeoApi.Data.Repository.v1
{
    public class LocalizationRequestRepository : Repository<Localization>, ILocalizationRequestRepository
    {
        public LocalizationRequestRepository(LocalizationRequestContext localizationRequestContext) : base(localizationRequestContext)
        {
        }

        public async Task<List<Localization>> GetLocalizationRequestsAsync(CancellationToken cancellationToken)
        {
            return await LocalizationRequestContext.LocalizationRequest.ToListAsync(cancellationToken);
        }

        public async Task<Localization> GetLocalizationRequestByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await LocalizationRequestContext.LocalizationRequest.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
