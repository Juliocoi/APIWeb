using APIWeb.Domain.Client;
using APIWeb.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.EndPoints.Clients;

public class ClientGetAll
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    public static IResult Action([FromQuery] int? page, [FromQuery] int? rows, ApplicationDbContext context)
    {
        if(page == null)
            page = 1;
        if(rows == null)
            rows = 10;
        
        var query = context.Clients.AsNoTracking().Include(c => c.City).OrderBy(c => c.Name);
        
        var queryFilter = query.Skip((page.Value - 1) * rows.Value).Take(rows.Value);

        var clients = query.ToList();

        var response = clients.Select(c => new ClientResponse(c.Id, c.Name, c.Sexo, c.Birthday, 
                        c.Idade, c.City.Name, c.City.State));

        return Results.Ok(response);
    }
}
