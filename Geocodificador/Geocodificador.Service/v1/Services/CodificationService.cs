using Geocodificador.Service.v1.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Geocodificador.Service.v1.Services
{
    public class CodificationService : ICodificationService
    {
        private readonly IMediator _mediator;

        public CodificationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public void CodificateLocalization(LocalizationRequestModel localizationRequestModel)
        {
            try
            {
                var localization = localizationRequestModel;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}
