using RudderstackForms.Models;
using RudderstackForms.Services.FormTemplates;
using RudderstackForms.Services.Sources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RudderstackDatabaseSettings>(
    builder.Configuration.GetSection("RudderstackDatabase"));

builder.Services.AddSingleton<FormTemplatesService>();
builder.Services.AddSingleton<SourcesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
