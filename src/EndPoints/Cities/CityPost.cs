using APIWeb.Domain.City;
using APIWeb.Infra.Data;

namespace APIWeb.EndPoints.Cities;

public class CityPost
{
    public static string Template => "/cities";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString(), };
    public static Delegate Handle => Action;

    public static IResult Action(CityRequest cityRequest, ApplicationDbContext context)
    {
        var city = new City(cityRequest.Name, cityRequest.State);

        if (!city.IsValid)
        {
            return Results.ValidationProblem(city.Notifications.ConvertToPromblemDetails());
        }
            
        

        context.Cities.Add(city);
        context.SaveChanges();

        return Results.Created($"/cities/{city.Id}", city.Id);
    }
}
