using EngieGemChallengeApi.Core.Component;
using EngieGemChallengeApi.Mapper;
using EngieGemChallengeApi.Models.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Injection Component
builder.Services.AddScoped<IProductionEnergieComponent, ProductionEnergieComponent>();
#endregion

#region Injection Validator
builder.Services.AddValidatorsFromAssemblyContaining<ProductionEnergieRequestValidator>();
#endregion


#region Injection Mapper
builder.Services.AddAutoMapper(typeof(ProductionEnergieMapper));
builder.Services.AddScoped<IProductionEnergieMapper, ProductionEnergieMapper>();
#endregion

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
