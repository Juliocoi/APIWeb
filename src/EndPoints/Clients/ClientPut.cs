using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Clients;

public class ClientPut
{
    public static string Template => "/clients/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, ClientRequest clientRequest, ApplicationDbContext context)
    {
        var client = context.Clients.Find(Id);

        if(client == null)
        {
            return Results.NotFound();
        }

        client.ClientUpdate(clientRequest.Name, clientRequest.Sexo, clientRequest.Birthday,
            clientRequest.Idade, clientRequest.CityId);

        context.SaveChanges();

        return Results.Ok();
    }
}
