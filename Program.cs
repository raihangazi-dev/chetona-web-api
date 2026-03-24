using System.ComponentModel;

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

app.MapGet("/api/v1/category", () =>
{
    // var response = new {status = "Success", message = "Respnse for get all category"};
    // return response;

    return Results.Ok(Categories);

});

app.MapPost("/api/v1/category", () =>
{
    var newCategory = new Category
    {
        Id = Guid.NewGuid(),
        Name = "History",
        Description = "Know about the history",
        CreatedAt = DateTime.UtcNow
    };

    Categories.Add(newCategory);

    return Results.Created($"/api/v1/category/{newCategory.Id}", newCategory);
});

app.MapPut("/api/v1/category", () =>
{
    var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Guid.Parse("693dfed0-4d03-49f8-8f63-2385b679f27f"));
    if(foundedCategory == null)
    {
        return Results.NotFound("Category with this Id does not Exist");
    }

    foundedCategory.Name = "Updated Name";
    foundedCategory.Description = "Updated Description";

    return Results.NoContent();
});

app.MapDelete("/api/v1/category", () =>
{
    var foundedCategory = Categories.FirstOrDefault(Category => Category.Id == Guid.Parse("693dfed0-4d03-49f8-8f63-2385b679f27f"));

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