using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonApp.Application.Pokemons
{
    public class GetPokemonByNameQueryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sprite { get; set; }
    }
}
