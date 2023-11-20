using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.EndPoints.Clients;

public class ClientGetById
{
    public static string Template => "/clients/{id:guid}";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var query = context.Clients.Include(c => c.City).Where(c => c.Id == id);

        var client = query.FirstOrDefault();

        if (client == null)
        {
            return Results.NotFound();
        }

        var response = new ClientResponse(client.Id, client.Name, client.Sexo,
            client.Birthday, client.Idade, client.City.Name, client.City.State);

        return Results.Ok(response);
    }
}
