using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Cities;

public class CityGetByState
{
    public static string Template => "/citiesbystate";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] string name, [FromQuery]string state, ApplicationDbContext context)
    {
        var city = context.Cities.FirstOrDefault(c => c.State == state && c.Name == name);

        if (city == null)
            return Results.NotFound();

        var response = new CityResponse(city.Id, city.Name, city.State);

        return Results.Ok(response);
    }
}
