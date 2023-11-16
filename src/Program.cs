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
app.MapMethods(CityGetAll.Template, CityGetAll.Methods, CityGetAll.Handle);
app.MapMethods(CityGetByName.Template, CityGetByName.Methods, CityGetByName.Handle);
app.MapMethods(CityGetByState.Template, CityGetByState.Methods, CityGetByState.Handle);
app.MapMethods(CityPut.Template, CityPut.Methods, CityPut.Handle);
app.MapMethods(CityDelete.Template, CityDelete.Methods, CityDelete.Handle);

app.Run();
