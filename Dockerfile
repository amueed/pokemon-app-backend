FROM mcr.microsoft.com/dotnet/core/sdk:3.1.419 AS build-env

WORKDIR /app

COPY ["PokemonApp.sln", "nuget.config", "./"]

COPY ["src/*/*.csproj", "src/"]
RUN for file in $(ls src/*.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

COPY ["tests/*/*.csproj", "tests/"]
RUN for file in $(ls tests/*.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

# Copy everything else and build
COPY . ./
RUN dotnet restore PokemonApp.sln
RUN dotnet publish "src/PokemonApp.WebAPI/PokemonApp.WebAPI.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "PokemonApp.WebAPI.dll"]