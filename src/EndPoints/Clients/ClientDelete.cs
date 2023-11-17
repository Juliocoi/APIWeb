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

        context.ChangeTracker.DetectChanges();
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        if (client == null)
        {
            return Results.NotFound();
        }

        context.Remove(client);

        context.ChangeTracker.DetectChanges();
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        context.SaveChanges();

        return Results.Ok(client);
    }
}
