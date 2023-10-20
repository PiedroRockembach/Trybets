FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/dist .
EXPOSE 5502

ENTRYPOINT ["dotnet", "TryBets.Matches.dll"]