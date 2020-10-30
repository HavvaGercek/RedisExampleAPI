FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY UserProject.API/*.csproj ./RedisExampleAPI/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY UserProject.API/. ./RedisExampleAPI/

#
WORKDIR /app/RedisExampleAPI
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/RedisExampleAPI/out ./
ENTRYPOINT ["dotnet", "RedisExampleAPI.dll"]