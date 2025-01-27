using Boilerplate.PublicApi.Data;
using Boilerplate.PublicApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new KeyNotFoundException("DefaultConnection");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddEndpoints();

builder.Services.AddSwaggerGen(opt => { });

builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseNpgsql(connectionString); });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Server is running at port 7285");
app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();

public partial class Program { }