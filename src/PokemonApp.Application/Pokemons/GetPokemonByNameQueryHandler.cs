using MediatR;
using Microsoft.Extensions.Caching.Memory;
using PokemonApp.Application.Exceptions;
using PokemonApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonApp.Application.Pokemons
{
    public class GetPokemonByNameQueryHandler : IRequestHandler<GetPokemonByNameQuery, GetPokemonByNameQueryResponse>
    {
        private readonly IPokemonService _pokemonService;
        private readonly ITranslationService _translationService;
        public GetPokemonByNameQueryHandler(IPokemonService pokemonService, ITranslationService translationService)
        {
            _pokemonService = pokemonService;
            _translationService = translationService;
        }
        public async Task<GetPokemonByNameQueryResponse> Handle(GetPokemonByNameQuery request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonService.GetPokemonAsync(request.Name);
            if (pokemon is null)
            {
                throw new NotFoundException($"Nothing found for this Search Text: {request.Name}");
            }

            var queryResponse = new GetPokemonByNameQueryResponse { Name = pokemon.Name, Sprite = pokemon.Sprite, Description = pokemon.Description };

            var translated = await _translationService.GetTranslationAsync(pokemon.Description);
            if (!string.IsNullOrEmpty(translated))
            {
                queryResponse.Description = translated;
            }
            return queryResponse;
        }
    }
}
