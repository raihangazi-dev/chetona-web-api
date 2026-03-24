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
app.MapGet("", () =>
{
    return Results.Ok("Welcome to Chetona Prokashon Web Server");
});

app.MapGet("/api/v1/category", () =>{});
app.MapPost("/api/v1/category",() => {});
app.MapPut("/api/v1/category", () => {});
app.MapDelete("/api/v1/category", () => {});

app.Run();
