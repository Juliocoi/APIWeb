namespace APIWeb.Domain.City;
using APIWeb.Domain.Client;


public class City: Entity
{
    public string Name { get; set; }
    public string State { get; set; }

    public ICollection<Client> Clients { get; }

    public City(string name, string state)
    {
        this.Name = name;
        this.State = state;
    }
    public void UpdateCity(string name, string state)
    {
        this.Name = name;
        this.State = state;
    }
}
