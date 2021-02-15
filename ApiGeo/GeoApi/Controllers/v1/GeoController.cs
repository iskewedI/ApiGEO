using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using GeoApi.Domain.Entities;
using GeoApi.Models.v1;
using GeoApi.Service.v1.Command;
using GeoApi.Service.v1.Query;
using MediatR;
using System.Collections.Generic;
using GeoApi.Service.v1.Models;

namespace GeoApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GeoController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to retrieve all localization requests
        /// </summary>
        /// <returns>Returns a list of all localization requests</returns>
        /// <response code="200">Returned if the list of localization requests was retrieved</response>
        /// <response code="400">Returned if the list of localization requests could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<Localization>>> LocalizationRequests()
        {
            try
            {
                return await _mediator.Send(new GetLocalizationRequestsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to localizate geographically a direction
        /// </summary>
        /// <param name="calle"></param>
        /// <param name="numero"></param>
        /// <param name="ciudad"></param>
        /// <param name="codigo_postal"></param>
        /// <param name="provincia"></param>
        /// <param name="pais"></param>
        /// <returns>The ID of the requested operation</returns>
        /// <response code="202">Returned if the operation has been Accepted</response>
        /// <response code="422">Returned when the parameter validation has failed</response>
        /// <response code="500">Returned if the operation has failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<LocalizationResponse>> LocalizationRequest(CreateLocalizationRequestModel createGeolocalizationRequestModel)
        {
            try
            {
                Localization request = _mapper.Map<Localization>(createGeolocalizationRequestModel);

                var newRequest = await _mediator.Send(new CreateLocalizationRequestCommand
                {
                    LocalizationRequest = request
                });

                return _mapper.Map<LocalizationResponse>(newRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
