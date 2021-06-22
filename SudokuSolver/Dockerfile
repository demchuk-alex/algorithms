FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SudokuSolver/SudokuSolver.csproj", "SudokuSolver/"]
RUN dotnet restore "SudokuSolver/SudokuSolver.csproj"
COPY . .
WORKDIR "/src/SudokuSolver"
RUN dotnet build "SudokuSolver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SudokuSolver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SudokuSolver.dll"]
