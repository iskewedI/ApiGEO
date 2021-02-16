using System;
using System.Threading.Tasks;
using AutoMapper;
using Geocodificador.Domain.Entities;
using Geocodificador.Models.v1;
using Geocodificador.Service.v1.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Geocodificador.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class CodificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CodificationController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new order in the database.
        /// </summary>
        /// <param name="orderModel">Model to create a new order</param>
        /// <returns>Returns the created order</returns>
        /// <response code="200">Returned if the order was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Codification>> Codification(CodificationModel orderModel)
        {
            try
            {
                return await _mediator.Send(new CodificateCommand
                {
                    Codification = _mapper.Map<Codification>(orderModel)
            });
        }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
