namespace APIWeb.EndPoints.Clients;

public record ClientResponse(Guid Id, string Name, string Sexo, DateTime birthday, int Idade, Guid CityId );
