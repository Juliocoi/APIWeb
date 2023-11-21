using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Cities;

public class CityDelete
{
    public static string Template => "/cities/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var city = context.Cities.Find(id);

        if (city == null)
            return Results.NotFound();

        context.Remove(city);
        context.SaveChanges();

        return Results.Ok();
    }
}
