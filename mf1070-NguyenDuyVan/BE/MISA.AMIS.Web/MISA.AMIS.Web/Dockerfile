#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MISA.AMIS.Web/MISA.AMIS.Web.csproj", "MISA.AMIS.Web/"]
RUN dotnet restore "MISA.AMIS.Web/MISA.AMIS.Web.csproj"
COPY . .
WORKDIR "/src/MISA.AMIS.Web"
RUN dotnet build "MISA.AMIS.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MISA.AMIS.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MISA.AMIS.Web.dll"]