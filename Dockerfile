# Use a imagem base do SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copie os arquivos do projeto e restaure as dependências
COPY *.sln .
COPY Catalogo.API/*.csproj ./Catalogo.API/
COPY Catalogo.CrossCutting/*.csproj ./Catalogo.CrossCutting/
COPY Catalogo.Domain/*.csproj ./Catalogo.Domain/
COPY Catalogo.Infrastructure/*.csproj ./Catalogo.Infrastructure/
RUN dotnet restore

# Copie o restante do código e faça o build
COPY . .
WORKDIR /app/Catalogo.API
RUN dotnet publish -c Release -o out

# Use a imagem base do runtime do .NET para execução
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/Catalogo.API/out .

# Exponha a porta usada pela API
EXPOSE 5000
EXPOSE 5001

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "Catalogo.API.dll"]