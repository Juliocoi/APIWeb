namespace APIWeb.Domain.Client;
using APIWeb.Domain.City;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Sexo { get; set; }
    public DateTime Birthday { get; set; }
    public int Idade { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; }

    public Client() { }

    public Client(string name, string sexo, DateTime birthday, int idade, City city)
    {
        Id = Guid.NewGuid();

        this.Name = name;
        this.Sexo = sexo;
        this.Birthday = birthday;
        this.Idade = idade; 
        this.City = city;
    }

    public void ClientUpdate(string name, string sexo, DateTime birthdaty, int idade, Guid cityId)
    {
        this.Name = name;
        this.Sexo = sexo;
        this.Birthday = birthdaty;
        this.Idade = idade;
        this.CityId = cityId;
    }
}   
