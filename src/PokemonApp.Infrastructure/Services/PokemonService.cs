using PokeApiNet;
using PokemonApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokemonApp.Infrastructure.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly PokeApiClient pokeClient;
        public PokemonService()
        {
            pokeClient = new PokeApiClient();

        }

        public async Task<PokemonResponse> GetPokemonAsync(string name)
        {
            try
            {
                var pokemonResponse = await pokeClient.GetResourceAsync<Pokemon>(name);
                var pokemonSpeciesResponse = await pokeClient.GetResourceAsync<PokemonSpecies>(name);
                return CreatePokemonResponse(pokemonResponse, pokemonSpeciesResponse);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Response status code does not indicate success: 404 (Not Found)."))
                {
                    return null;
                }
                throw;
            }
        }

        private static PokemonResponse CreatePokemonResponse(Pokemon pokemon, PokemonSpecies pokemonSpecies)
        {
            var pokemonName = pokemonSpecies.Name;
            var description = GetDescription(pokemonSpecies);
            var sprite = GetSpriteImage(pokemon);

            return new PokemonResponse
            {
                Name = pokemonName,
                Description = description,
                Sprite = sprite
            };
        }

        private static string GetDescription(PokemonSpecies pokemonSpecies)
        {
            return pokemonSpecies.FlavorTextEntries
                .Where(flavorTexts => flavorTexts.Language.Name == "en")
                .Select(ParseLineBreaks)
                .FirstOrDefault();
        }

        private static string GetSpriteImage(Pokemon pokemon)
        {
            return pokemon.Sprites.FrontDefault;
        }

        private static string ParseLineBreaks(PokemonSpeciesFlavorTexts flavorTexts)
            => Regex.Replace(flavorTexts.FlavorText, @"\t|\n|\r|\f", " ");
    }
}
