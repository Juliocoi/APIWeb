using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Cities;

public class CityPut
{
    public static string Template => "/cities/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id, CityRequest cityRequest, ApplicationDbContext context)
    {
        var city = context.Cities.Find(id);

        if (city == null)
            return Results.BadRequest();

        city.UpdateCity(cityRequest.Name, cityRequest.State);

        context.SaveChanges();

        return Results.Ok();
    }
}
