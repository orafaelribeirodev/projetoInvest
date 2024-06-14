using GestaoPortifolioInvestimento.Application.Service;
using GestaoPortifolioInvestimento.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GestaoPortifolioInvestimento.Repositories.ICustomerData, GestaoPortifolioInvestimento.Repositories.CustomerData>();
builder.Services.AddScoped<GestaoPortifolioInvestimento.Application.ICustomerService, GestaoPortifolioInvestimento.Application.CustomerService>();
// Adicionar servi�os ao cont�iner de inje��o de depend�ncia.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Configurar o pipeline de requisi��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Investment Portfolio API V1");
    });
}

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin());
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
