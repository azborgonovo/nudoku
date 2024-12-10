using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nudoku.Engine;
using Nudoku.Engine.Generators;
using Nudoku.Engine.Solvers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPuzzleGenerator, PuzzleGeneratorV1>();
builder.Services.AddSingleton<ISolver, SolverV1>();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(options => { options.WithTitle("Nudoku API: The Ninjacat of Sudoku"); });
app.UseHttpsRedirection();

app.MapPost("/solve", (GridDto puzzle, [FromServices] ISolver solver) =>
{
    var puzzleGrid = new Grid(puzzle.Size, puzzle.BoxWidth, puzzle.BoxHeight, puzzle.Cells);
    
    var solutionGrid = solver.FindSolution(puzzleGrid);
    
    if (solutionGrid is null)
        return Results.BadRequest("No solution found");

    var solution = new GridDto(solutionGrid.Size,
        solutionGrid.BoxWidth,
        solutionGrid.BoxHeight,
        solutionGrid.Cells.ToArray());

    return TypedResults.Ok(solution);

}).WithName("FindFirstSolution");

app.MapPost("/generate", (GeneratePuzzleRequest request, [FromServices] IPuzzleGenerator generator) =>
{   
    var puzzle = generator.Generate(request.BoxWidth, request.BoxHeight);

    var solution = new GridDto(puzzle.Size,
        puzzle.BoxWidth,
        puzzle.BoxHeight,
        puzzle.Cells.ToArray());

    return TypedResults.Ok(solution);

}).WithName("GeneratePuzzle");

await app.RunAsync();

record GridDto(int Size, int BoxWidth, int BoxHeight, int[] Cells);

record GeneratePuzzleRequest(int BoxWidth, int BoxHeight);