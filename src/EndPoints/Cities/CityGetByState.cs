using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Cities;

public class CityGetByState
{
    public static string Template => "/cities-by-state";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery]string state, ApplicationDbContext context)
    {
        var cities = context.Cities.Where(c => c.State.Contains(state));

        if (!cities.Any())
            return Results.NotFound();

        var response = cities.Select(c => new CityResponse(c.Id, c.Name, c.State));

        return Results.Ok(response);
    }
}
