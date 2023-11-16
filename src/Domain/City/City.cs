namespace APIWeb.Domain.City;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }

    public City(string name, string state)
    {
        Id = Guid.NewGuid();
        Name = name;
        State = state;
    }
}
