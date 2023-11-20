using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Clients;

public class ClientDelete
{
    public static string Template => "/clients/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,ApplicationDbContext context)
    {
        var client = context.Clients.Find(id);

        if (client == null)
        {
            return Results.NotFound();
        }

        context.Remove(client);
        context.SaveChanges();

        return Results.Ok();
    }
}
