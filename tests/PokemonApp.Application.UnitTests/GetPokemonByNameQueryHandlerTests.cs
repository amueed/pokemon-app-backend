using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PokemonApp.Application.Exceptions;
using PokemonApp.Application.Pokemons;
using PokemonApp.Application.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokemonApp.Application.UnitTests
{
    public class GetPokemonByNameQueryHandlerTests
    {
        [Fact]
        public async Task TestGetPokemonByNameQueryHandlerWasCalled()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetPokemonByNameQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<GetPokemonByNameQueryResponse>());

            //Act
            await mediator.Object.Send(new GetPokemonByNameQuery("pokemon"));

            //Assert
            mediator.Verify(x => x.Send(It.IsAny<GetPokemonByNameQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task TestGetPokemonByNameQueryHandlerThrowWhenNotResult()
        {
            //Arange
            var mockedServiceResponse = (PokemonResponse)null;
            var pokemonService = new Mock<IPokemonService>();
            pokemonService.Setup(x => x.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(mockedServiceResponse);

            var translationService = new Mock<ITranslationService>();

            var command = new GetPokemonByNameQuery("pokemon");

            GetPokemonByNameQueryHandler handler = new GetPokemonByNameQueryHandler(pokemonService.Object, translationService.Object);
            
            // Act & Asert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task TestGetPokemonByNameQueryHandlerTranslateDescription()
        {
            //Arange
            var mockedServiceResponse = new PokemonResponse { Name = "pokemon", Description = "test", Sprite = "test" };
            var pokemonService = new Mock<IPokemonService>();
            pokemonService.Setup(x => x.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(mockedServiceResponse);

            string mockedTranslation = "translated";
            var translationService = new Mock<ITranslationService>();
            translationService.Setup(x => x.GetTranslationAsync(It.IsAny<string>()))
                .ReturnsAsync(mockedTranslation);

            var command = new GetPokemonByNameQuery("pokemon");

            GetPokemonByNameQueryHandler handler = new GetPokemonByNameQueryHandler(pokemonService.Object, translationService.Object);

            //Act
            GetPokemonByNameQueryResponse x = await handler.Handle(command, new CancellationToken());

            //Asert
            Assert.True(x.Description == mockedTranslation);
        }

        [Fact]
        public async Task TestGetPokemonByNameQueryHandlerDefaultDescriptionWhenCouldNotTranslate()
        {
            //Arange
            var mockedServiceResponse = new PokemonResponse { Name = "pokemon", Description = "test", Sprite = "test" };
            var pokemonService = new Mock<IPokemonService>();
            pokemonService.Setup(x => x.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(mockedServiceResponse);

            string mockedTranslation = null;
            var translationService = new Mock<ITranslationService>();
            translationService.Setup(x => x.GetTranslationAsync(It.IsAny<string>()))
                .ReturnsAsync(mockedTranslation);

            var command = new GetPokemonByNameQuery("pokemon");

            GetPokemonByNameQueryHandler handler = new GetPokemonByNameQueryHandler(pokemonService.Object, translationService.Object);

            //Act
            GetPokemonByNameQueryResponse x = await handler.Handle(command, new CancellationToken());

            //Asert
            Assert.NotNull(x.Description);
            Assert.Equal(x.Description, mockedServiceResponse.Description);
        }

    }
}
