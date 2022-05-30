using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonApp.Application.Pokemons
{
    public class GetPokemonByNameQuery : IRequest<GetPokemonByNameQueryResponse>
    {
        public string Name { get; }

        public GetPokemonByNameQuery(string name)
        {
            Name = name;
        }
    }
}
