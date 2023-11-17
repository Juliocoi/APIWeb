using APIWeb.Domain.Client;
using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.EndPoints.Clients;

public class ClientGetAll
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    
    public static IResult Action(ApplicationDbContext context)
    {
        var clients = context.Clients.AsNoTracking().Include(c => c.City).ToList();

        var response = clients.Select(c => new ClientResponse(c.Id, c.Name, c.Sexo, c.Birthday, 
                        c.Idade, c.CityId));

        return Results.Ok(response);
    }
}
