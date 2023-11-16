namespace APIWeb.Domain.City;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }

    public City(string name, string state)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.State = state;
    }

    public void UpdateCity(string name, string state)
    {
        this.Name = name;
        this.State = state;
    }
}
