FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app

COPY . ./

RUN dotnet publish "todoapi.csproj" -c Release -o /app/publish \
    --runtime alpine-x64 \
    --self-contained true \
    /p:PublishTrimmed=true \
    /p:TrimMode=Link \
    /p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./todoapi"]