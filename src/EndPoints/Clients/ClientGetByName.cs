using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Clients;

public class ClientGetByName
{
    public static string Template => "/clientsbyname";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] string name, ApplicationDbContext context)
    {
        var client = context.Clients.FirstOrDefault(c => c.Name == name);

        if (client == null)
            return Results.NotFound();

        var response = new ClientResponse(client.Id, client.Name, client.Sexo,
            client.Birthday, client.Idade, client.CityId);

        return Results.Ok(response);
    }
}
