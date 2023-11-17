using APIWeb.Domain.Client;
using APIWeb.Infra.Data;

namespace APIWeb.EndPoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString(), };
    public static Delegate Handle => Action;

    public static IResult Action(ClientRequest clientRequest, ApplicationDbContext context)
    {
        var city = context.Cities.FirstOrDefault(c => c.Id == clientRequest.CityId);

        var client = new Client(clientRequest.Name, clientRequest.Sexo, 
            clientRequest.Birthday, clientRequest.Idade, city);

        context.Clients.Add(client);
        context.SaveChanges();

        return Results.Created($"/clients/{client.Id}", client.Id);
    }
}
