using APIWeb.EndPoints.Cities;
using APIWeb.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
// ########## Configuração banco de dados
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionString:APIWebDb"]);

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

app.MapMethods(CityPost.Template, CityPost.Methods, CityPost.Handle);
app.Run();
