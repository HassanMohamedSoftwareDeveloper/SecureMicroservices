using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDbContext<MoviesAPIContext>(
    options => options.UseInMemoryDatabase("MoviesDB"));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.SeedAsync();
app.Run();
