namespace APIWeb.Domain.Client;
using APIWeb.Domain.City;
using Flunt.Notifications;
using Flunt.Validations;


public class Client: Notifiable<Notification>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Sexo { get; private set; }
    public DateTime Birthday { get; private set; }
    public int Idade { get; private set; }

    public Guid CityId { get; private set; }
    public City City { get; private set; }

    public Client() { }

    public Client(string name, string sexo, DateTime birthday, int idade, City city)
    {
        Id = Guid.NewGuid();

        this.Name = name;
        this.Sexo = sexo;
        this.Birthday = birthday;
        this.Idade = idade; 
        this.City = city;

        Validate();
    }

    public void ClientUpdate(string name, string sexo, DateTime birthdaty, int idade, Guid cityId)
    {
        this.Name = name;
        this.Sexo = sexo;
        this.Birthday = birthdaty;
        this.Idade = idade;
        this.CityId = cityId;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Client>()
            .IsNotNullOrEmpty(Name, "Name", "O nome é obrigatório")
            .IsGreaterOrEqualsThan(Name, 2, "Name", "Nome invalido")
            .IsNotNullOrEmpty(Sexo, "Sexo", "O nome é obrigatório")
            .IsGreaterOrEqualsThan(Sexo, 2, "Sexo", "Nome invalido")
            .IsNotNull(Birthday, "Birthday", "A data de nascimento é obrigatório")
            .IsNotNull(Idade, "Idade", "O campo idade é obrigatório");
        AddNotifications(contract);
    }
}   
