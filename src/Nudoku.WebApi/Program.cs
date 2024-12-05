using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nudoku.Engine;
using Nudoku.Engine.Solvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ISolver, SolverV2>();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/solve", (GridDto puzzle, [FromServices] ISolver solver) =>
{
    var puzzleGrid = new Grid(puzzle.Size, puzzle.Cells);
    
    var solutionGrid = solver.FindSolution(puzzleGrid);
    
    if (solutionGrid is null)
        return Results.BadRequest("No solution found");

    var solution = new GridDto(solutionGrid.Size,
        solutionGrid.BoxWidth,
        solutionGrid.BoxHeight,
        solutionGrid.Cells.Cast<int>().ToArray());

    return TypedResults.Ok(solution);

}).WithName("FindFirstSolution");

await app.RunAsync();

record GridDto(int Size, int BoxWidth, int BoxHeight, int[] Cells);