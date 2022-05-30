using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonApp.Application.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonApp.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IMediator _mediator;

        public PokemonController(ILogger<PokemonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{pokemonName}")]
        public async Task<ActionResult<GetPokemonByNameQueryResponse>> Get([FromRoute] string pokemonName)
        {
            var result = await _mediator.Send(new GetPokemonByNameQuery(pokemonName));
            return Ok(result);
        }
    }
}
