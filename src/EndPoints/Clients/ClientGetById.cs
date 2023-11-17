using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Clients;

public class ClientGetById
{
    public static string Template => "/clients/{id:guid}";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, ApplicationDbContext context)
    {
        var client = context.Clients.Find(Id);

        if(client == null)
        {
            return Results.NotFound();
        }

        var response = new ClientResponse(client.Id, client.Name, client.Sexo, 
            client.Birthday, client.Idade, client.CityId);

        return Results.Ok(response);
    }
}
