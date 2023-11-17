namespace APIWeb.EndPoints.Clients;

public record ClientRequest(string Name, string Sexo, DateTime Birthday, int Idade, Guid CityId);
