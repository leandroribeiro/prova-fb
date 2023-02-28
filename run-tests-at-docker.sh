docker run \
  --rm \
  -v $(pwd):/src \
  -w /src \
  mcr.microsoft.com/dotnet/sdk:6.0 \
  dotnet test ./src/mars-robot.sln --logger:"console;verbosity=detailed" 