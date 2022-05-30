using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApp.Application.Services
{
    public class TranslationResponse
    {
        public TranslationContent Contents { get; set; }
    }

    public class TranslationContent
    {
        public string Translated { get; set; }
    }

    public interface ITranslationService
    {
        Task<string> GetTranslationAsync(string phrase);
    }
}
