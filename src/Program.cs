using APIWeb.EndPoints.Cities;
using APIWeb.EndPoints.Clients;
using APIWeb.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
// ########## Configuração banco de dados
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionString:APIWebDb"]);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.MapMethods(ClientPost.Template, ClientPost.Methods, ClientPost.Handle);
app.MapMethods(ClientGetAll.Template, ClientGetAll.Methods, ClientGetAll.Handle);
app.MapMethods(ClientGetById.Template, ClientGetById.Methods, ClientGetById.Handle);
app.MapMethods(ClientGetByName.Template, ClientGetByName.Methods, ClientGetByName.Handle);
app.MapMethods(ClientPut.Template, ClientPut.Methods, ClientPut.Handle);
app.MapMethods(ClientDelete.Template, ClientDelete.Methods, ClientDelete.Handle);

app.UseCors("corsPolicy");
app.Run();
