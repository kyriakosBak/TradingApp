# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md

# Create a stage for building the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

COPY . /source

WORKDIR /source

# Build the application.
# Leverage a cache mount to /root/.nuget/packages so that subsequent builds don't have to re-download packages.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
WORKDIR /app

# Copy everything needed to run the app from the "build" stage.
COPY --from=build /app .

# Create a non-privileged user that the app will run under.
# See https://docs.docker.com/go/dockerfile-user-best-practices/
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser
USER appuser

ENTRYPOINT ["dotnet", "TradingApp.Frontend.dll"]
