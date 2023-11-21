namespace APIWeb.Domain.City;
using APIWeb.Domain.Client;
using Flunt.Notifications;
using Flunt.Validations;

public class City: Notifiable<Notification>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string State { get; private set; }

    public ICollection<Client> Clients { get; }

    public City(string name, string state)
    {
        Id = Guid.NewGuid();
        this.Name = name;
        this.State = state;

        Validate();
    }
    public void CityUpdate(string name, string state)
    {
        this.Name = name;
        this.State = state;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<City>()
            .IsNotNullOrEmpty(Name, "Name", "O nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 2, "Name", "Nome inválido")
            .IsNotNullOrEmpty(State, "State", "O nome é obrigatório")
            .IsGreaterOrEqualsThan(State, 2, "State", "Nome invárlido");
        AddNotifications(contract);
    }
}
