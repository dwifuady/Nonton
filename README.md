# Nonton

A [Stremio](https://www.stremio.com/) Client built with Blazor

![GitHub repo size](https://img.shields.io/github/repo-size/dwifuady/Nonton?color=%234682B4)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/dwifuady/Nonton?color=%23483D8B)
![Github Page](https://github.com/dwifuady/Nonton/actions/workflows/deploy-gh-page.yml/badge.svg)

Nonton is a side-project for me to experiment with a few technologies that interest me.  
It is a [Blazor WebAssembly](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) application and hosted in this Github page [https://dwifuady.github.io/Nonton/](https://dwifuady.github.io/Nonton/)

Nonton is not a Stremio replacement, but I am trying to implement every features on the Stremio to Nonton.  
Nonton didn't have it's own back-end service, it needs addons to works. Every addon is installed locally in the browser cache.

## Run Locally
### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.400-windows-x64-installer)
- [.NET WebAssembly build tools](https://docs.microsoft.com/en-us/aspnet/core/blazor/webassembly-native-dependencies?view=aspnetcore-6.0)  
You can install by running ```dotnet workload install wasm-tools``` in a command shell
- [nodejs](https://nodejs.org/en/) & [tailwindcss](https://tailwindcss.com/)

### Running the app
- Use your favorite IDE (VS or JetBrains Rider), or
- Navigate to the root of this project, and run below command in a command shell  
```dotnet run --project .\Nonton\Nonton.csproj```
- For tailwind, navigate to Nonton csproj directory, and run  
```npx tailwindcss -i .\Styles\app.css -o .\wwwroot\css\app.css --watch```
- Open ```https://localhost:7172/``` on your browser


## Running the app using Docker
- Navigate to the root of this project
- Build the image ```docker build -t nonton .```
- Run ```docker run -d -p 7172:80 nonton```
- Open ```https://localhost:7172/``` on your browser
  
---
Nonton does not provide any content, all contents displayed are responsibility of the addons creators.  
Using third-party addons will always be subject to your responsibility and the governing law of the jurisdiction you are located.
