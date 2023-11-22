using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.EndPoints.Clients;

public class ClientGetByName
{
    public static string Template => "/clients-by-name";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] string name, ApplicationDbContext context)
    {
        var client = context.Clients.Include(c => c.City).Where(c => c.Name == name);

        if (!client.Any())
            return Results.NotFound();
        
        var response = client.Select(c => new ClientResponse(c.Id, c.Name, c.Sexo,
            c.Birthday, c.Idade, c.Name, c.City.State));

        return Results.Ok(response);
    }
}
