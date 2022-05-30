using Newtonsoft.Json;
using PokemonApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokemonApp.Infrastructure.Services
{
    public class TranslationService : ITranslationService
    {
        private const string RESOURCE_URL = @"https://api.funtranslations.com/translate/shakespeare";

        private readonly IHttpClientFactory _httpClientFactory;
        public TranslationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetTranslationAsync(string phrase)
        {
            var client = _httpClientFactory.CreateClient();
            var data = new Dictionary<string, string>();
            data.Add("text", phrase);

            var response = await client.PostAsync(RESOURCE_URL, new FormUrlEncodedContent(data));

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();

                var translationResponse = JsonConvert.DeserializeObject<TranslationResponse>(responseJson);

                return translationResponse?.Contents is null
                    ? null
                    : ParseDuplicatedWhitespaces(translationResponse);
            }
            return null;
        }

        private static string ParseDuplicatedWhitespaces(TranslationResponse translationResponse)
        {
            return Regex.Replace(translationResponse.Contents.Translated, @"[ ]{2,}", " ");
        }
    }
}
