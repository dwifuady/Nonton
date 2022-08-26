FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet workload install wasm-tools
RUN dotnet restore Nonton/Nonton.csproj
COPY . .
RUN apt-get update
RUN apt-get install -y python3
RUN dotnet build Nonton/Nonton.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Nonton/Nonton.csproj -c Release -o /app/publish --nologo

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY Nonton/nginx.conf /etc/nginx/nginx.conf