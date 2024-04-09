using Microsoft.Extensions.Options;
using YourProjectName.Interfaces;
using YourProjectName.Models;
using YourProjectName.Services;
// Add any other necessary using directives

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// Register the MongoDB service
builder.Services.AddSingleton<MachineService>();

builder.Services.AddControllers();
// Add other necessary services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
