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
        var query = context.Clients.Include(c => c.City).Where(c => c.Name == name);

        var client = query.FirstOrDefault();

        if (client == null)
            return Results.NotFound();

        var response = new ClientResponse(client.Id, client.Name, client.Sexo,
            client.Birthday, client.Idade, client.City.Name, client.City.State);

        return Results.Ok(response);
    }
}
