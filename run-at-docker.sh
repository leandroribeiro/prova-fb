docker run \
  --rm \
  -it \
  -v $(pwd):/src \
  -w /src \
  mcr.microsoft.com/dotnet/sdk:6.0 \
  dotnet run --project ./src/mars-robot.cli/mars-robot.cli.csproj