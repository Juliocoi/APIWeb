using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIWeb.EndPoints.Cities;

public class CityGetAll
{
    public static string Template => "/cities";
    public static string[] Methods => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action([FromQuery] int? page, [FromQuery]int? rows, string? orderBy, ApplicationDbContext context)
    {
        if (page == null) page = 1;
        if (rows == null || rows.Value > 10) rows = 10;
        if (string.IsNullOrEmpty(orderBy)) orderBy = "name";

        var querySearch = context.Cities.AsNoTracking();

        if (orderBy == "name")
            querySearch = querySearch.OrderBy(p => p.Name);
        else if (orderBy == "state")
            querySearch = querySearch.OrderBy(p => p.State);

        var queryFilter = querySearch.Skip((page.Value -1) * rows.Value).Take(rows.Value);

        var cities = queryFilter.ToList();

        var result = cities.Select(c => new CityResponse(c.Id, c.Name, c.State));
        
        return Results.Ok(result);
    }
}
