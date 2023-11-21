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
        var client = context.Clients.Include(c => c.City).Where(c => c.Id == id);

        if (client == null)
        {
            return Results.NotFound();
        }

        var response = client.Select(c => new ClientResponse(c.Id, c.Name, c.Sexo,
            c.Birthday, c.Idade, c.City.Name, c.City.State));

        return Results.Ok(response);
    }
}
