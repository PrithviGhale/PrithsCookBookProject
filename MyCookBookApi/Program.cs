using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyCookBookApi.Repositories;
using MyCookBookApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the repository and service layers
builder.Services.AddSingleton<IRecipeRepository, MockRecipeRepository>(); // Register MockRecipeRepository
builder.Services.AddScoped<IRecipeService, RecipeService>(); // Register RecipeService

// Configure CORS to allow the frontend to communicate with the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("https://localhost:5001") // Allow requests from the frontend
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend"); // Enable CORS
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();