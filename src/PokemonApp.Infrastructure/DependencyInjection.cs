using Microsoft.Extensions.DependencyInjection;
using PokemonApp.Application.Services;
using PokemonApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddSingleton<IPokemonService, PokemonService>()
                .AddSingleton<ITranslationService, TranslationService>();
        }
    }
}
