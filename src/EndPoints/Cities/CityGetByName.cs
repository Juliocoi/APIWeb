using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.EndPoints.Cities;

public class CityGetByName
{
    public static string Template => "/citiesbyname";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] string name, ApplicationDbContext context)
    {
        var city = context.Cities.FirstOrDefault(c => c.Name == name);

        if (city == null)
            return Results.NotFound();

        var response = new CityResponse(city.Id, city.Name, city.State);

        return Results.Ok(response);
    }
}
