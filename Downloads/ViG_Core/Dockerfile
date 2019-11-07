FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["VIG_CORE.csproj", ""]
RUN dotnet restore "./VIG_CORE.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "VIG_CORE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VIG_CORE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VIG_CORE.dll"]