FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/AlunosTech.Api -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
USER app
EXPOSE 8080
ENTRYPOINT ["dotnet", "AlunosTech.Api.dll"]
