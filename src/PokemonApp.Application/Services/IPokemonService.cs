using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApp.Application.Services
{
    public class PokemonResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sprite { get; set; }
    }

    public interface IPokemonService
    {
        Task<PokemonResponse> GetPokemonAsync(string name);
    }
}
