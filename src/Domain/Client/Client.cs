namespace APIWeb.Domain.Client;
using APIWeb.Domain.City;

public class Client : Entity
{
    public string Name { get; set; }
    public string Sexo { get; set; }
    public DateTime Birthday { get; set; }
    public int Idade { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; }
}   
