using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Basic CRUD API endpoints Creation
List<Category> Categories = new List<Category>();

app.MapGet("", () =>
{
    return Results.Ok("Welcome to Chetona Prokashon Web Server");
});

app.MapGet("/api/v1/category", ([FromQuery] string SearchValue = "") =>
{
    // var response = new {status = "Success", message = "Respnse for get all category"};
    // return response;

    if(!string.IsNullOrEmpty(SearchValue))
    {
        var foundedCategories = Categories.Where(Category => Category.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(foundedCategories);
    }

    Console.WriteLine($"{SearchValue}");

    return Results.Ok(Categories);

});

app.MapPost("/api/v1/category", ([FromBody] Category CategoryData) =>
{
    var newCategory = new Category
    {
        Id = Guid.NewGuid(),
        Name = CategoryData.Name,
        Description = CategoryData.Description,
        CreatedAt = DateTime.UtcNow
    };

    Categories.Add(newCategory);

    return Results.Created($"/api/v1/category/{newCategory.Id}", newCategory);
});

app.MapPut("/api/v1/category/{Id}", (Guid Id ,[FromBody] Category CategoryData) =>
{
    var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Id);
    if(foundedCategory == null)
    {
        return Results.NotFound("Category with this Id does not Exist");
    }

    foundedCategory.Name = CategoryData.Name;
    foundedCategory.Description = CategoryData.Description;

    return Results.NoContent();
});

app.MapDelete("/api/v1/category/{Id}", (Guid Id) =>
{
    var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Id);

    if(foundedCategory == null)
    {
        return Results.NotFound("Category with this Id does not exist");
    }

    Categories.Remove(foundedCategory);

    return Results.NoContent();
});

app.Run();

public record Category
{
    public Guid Id {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime CreatedAt {get; set;}
}