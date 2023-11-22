# APIWeb

## Sumário

1. [Configurações](#Config)
2. [Configure o banco de dados](#Configure-o-banco-de-dados)
3. [Criando banco de dados para o projeto](#Criando-um-banco-de-dados-para-o-projeto)
4. [Rodando o projeto](#Rodando-o-projeto)
5. [Criando uma Cidade](#Criando-uma-cidade)
6. [Listar todas as cidades](#Listar-todas-as-cidades)
7. [Pesquisar por nome da cidade](#Pesquisar-por-nome-da-cidade)
8. [Pesquisa pelo nome do Estado](#Pesquisa-pelo-nome-do-Estado)
9. [Atualizar Cidade](#Atualizar-Cidade)
10. [Deletar Cidade](#Deletar-Cidade)
11. [Criando um Cliente](#Criando um Cliente)
12. [Listar todos os clientes](#Listar-todos-os-clientes)
13. [Pesquisar cliente pelo ID](#Pesquisar cliente pelo ID)
14. [Pesquisar cliente pelo Nome](#Pesquisar-cliente-pelo-Nome)
15. [Atualizar Cliente](#Atualizar Cliente)
16. [Deletar Cliente](#Deletar-Cliente)
17. [Error](#Error)

## Config

* Clone o repositório do Github

  ```shell
  git clone https://github.com/Juliocoi/APIWeb.git
  cd APIWeb
  ```

* Baixe as dependências do projeto usando um dos comandos abaixo, segundo sua preferência.

  ```markdown
  * dotnet restore => Apenas restaura as dependências 
  * dotnet build => Restaura as dependências e faz o build do projeto
  * dotnet watch ou dotnet run =>  Restaura as dependências, faz o build do projeto e executa o projeto
  ```

## Configure o banco de dados

Caso não tenha um servidor configurado, você poderá subir um contêiner no [Docker]([Docker: Accelerated Container Application Development](https://www.docker.com/)).

Com o Docker instalado, Instale a imagem de um servidor MySql:

```shell
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Agora suba um contêiner usando a imagem baixada na etapa anterior:

```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" -p 1433:1433 --name sqlserver --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```

O parâmetro `--name` será o nome do container. 

O parâmetro `-e` será a senha do banco de dados.

E o parâmetro `-d` a porta usada pelo banco de dados.

O comando `docker ps` listará os containers que estarão em execução

[![image.png](https://i.postimg.cc/zXRNwqXV/image.png)](https://postimg.cc/0bxhPRSs)

Caso nenhum container esteja em execução, acione o container criado:

```shell
docker start sqlserver
```

## Criando um banco de dados para o projeto

Agora certifique-se de que está dentro da pasta do projeto e rode o comando abaixo para criar o banco de dados.

```shell
dotnet ef database update
```

Um banco de dados será criado seguindo as diretrizes dos arquivos de configurações do projeto. Essas configurações podem ser encontradas no arquivo `appsettings.json`, sob a tag `ConnectionString`.

```shell
"ConnectionString": {
  "APIWebDb": "Server=localhost;Database=APIWebDb;User Id=sa;Password=@Sql2022;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES"
},
```

O parâmetro `DataBase` indica o nome do banco de dados. Você pode alterá-lo segundo seus critérios.

`UserId  `e `senha `correspondem, respectivamente, ao seu usuário e senha criado na etapa anterior.

## Rodando o projeto

Para rodar o projeto use o comando:

```shell
dotnet watch run
```

O projeto será executado na em um servidor local na porta http://localhost:5237, essa informação pode ser encontrada no arquivo `./src/Properties/launchSettings.json[profiles]`.

## Criando uma Cidade

Devido a natureza do relacionamento existente entre as entidades do sistema, todo Cliente deve estar associado a uma Cidade. Por este motivo, é fundamental a criação dos registros das cidades.

Para criar uma cidade utilize a rota **/cities**:

`http://localhost:5237/cities`

A requisição será do tipo <b><span style = "color:yellow">POST</span></b>. O payload da requisição seguirá o padrão .JSON, no seguinte formato:

```json
{
  "name": "string",
  "state": "string"
}
```

Um ID do tipo **GUID** será criado para o registro no banco de dados automaticamente em tempo de execução.

Se o registro for efetuado com sucesso, você receberá o **HTTP status code 201** com um número do ID do usuário.

## Listar todas as cidades

Para listar todas as cidades existente no banco utilize uma requisição do tipo <b><span style = "color:green">GET</span></b> na rota **/cities**:

`http://localhost:5237/cities`

Para otimização da pesquisa em um banco com muitos registros, a rota possui três argumentos opcionais do tipo **query params**.

* **page**: Tipo Int. Indica o número da página que você deseja exibir e dependerá da quantidade de registro existente na tabela. Por padrão o valor será de 1, indicando a primeira página do registro;
* **rows**: Tipo Int. Indica a quantidades de registro que deseja por página, Padrão: 10. Max: 10;
* **orderby**: Tipo String. Escolher dentre as opções “name” e “state”. Por padrão: name.

Se escolher passar todos os parâmetros o endereço será:

`http://localhost:5237/cities?page=1&rows=10&orderBy=name`

O retorno será um HTTP status code 200 e um array de objetos:

```json
[
      {
        "id": "63c04e1a-02b0-425b-96ed-39ea8187c5c6",
        "name": "Salvador",
        "state": "Bahia"
    }
]
```

## Pesquisar por nome da Cidade

Requisição do tipo <b><span style = "color:green">**GET**</span></b>, na rota **/cities-by-name**, que **recebe um argumento do tipo string chamado name** que indica o nome da cidade que você deseja consultar. Endereço de exemplo

`http://localhost:5237/cities-by-name?name=NomeDaCidade`

Se a requisição retornar um sucesso será retornado um HTTP status code 200 e um array de objetos.

```json
[
    {
        "id": "52b9b60a-ab80-4821-95e6-d4db583ec937",
        "name": "São Paulo",
        "state": "São Paulo"
    }
]
```

Se o registro não existir no banco vc receberá um HTTP status code 404.

## Pesquisa pelo nome do Estado

Requisição do tipo <b><span style = "color:green">**GET**</span></b>, na rota **/cities-by-state**, que recebe um argumento do tipo **string** chamado **state** que indica o nome do estado que você deseja consultar. Endereço de exemplo:

`http://localhost:5237/cities-by-state?state=Pernambuco`

Se a requisição retornar um sucesso, retornará um HTTP status code 200 e um array de objetos.

```json
[
    {
        "id": "c5a1351d-9713-4fd5-b8ac-9fcb53b2ca10",
        "name": "João Pessoa",
        "state": "Paraiba"
    }
]
```

Se o parâmetro não for devidamente utilizado, ou seja, o parâmetro existe, mas nenhum valor foi informado, será retornado um HTTP status code 200 e um array de objetos contendo os registros das Cidades.

Se o parâmetro não for informado na rota será retornado um HTTP status code 500 e um objeto contendo o erro.

```json
{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
    "title": "Error to convert data",
    "status": 500
}
```

## Atualizar Cidade

Para atualizar uma cidade, enviar uma requisição do tipo <b><span style = "color:BLUE">**PUT**</span></b> no endereço **/cities/{id:guid}**.

A rota recebe um argumento obrigatório via **ROUTE**, que deve ser um GUID correspondente ao ID de alguma cidade do banco de dados. Para conseguir um ID vide os [Listar cidade](#Listar-todas-as-cidades).

Ex.:

`http://localhost:5237/cities/2381aaaa-ee45-433a-b0b9-a0c2b1a819f1`

O payload deverá ser enviado em pelo body da requisição utilizando o formato.JSON.

```json
{
    "Name": "Paulista",
    "State": "Pernambuco"
}
```

Sucesso: HTTP status code 200.

## Deletar Cidade

Para deletar uma cidade, enviar uma requisição do tipo <b><span style = "color:pink">**PUT**</span></b> no endereço **/cities/{id:guid}**.

A rota recebe um argumento obrigatório via **ROUTE**, que deve ser GUID correspondente ao ID de alguma cidade do banco de dados. Para conseguir um ID vide os [Listar cidade](#Listar-todas-as-cidades).

Ex.:

`http://localhost:5237/cities/2381aaaa-ee45-433a-b0b9-a0c2b1a819f1`

Sucess: HTTP status code 200.

Se nenhum ID for informado, será retornado um HTTP status code 405,

Se o ID informado não existir no banco será retornado um HTTP status code 404.

## Criando um Cliente

A requisição será do tipo <b><span style = "color:yellow">POST</span></b>. na rota **/clients**

`http://localhost:5237/clients`

O as informações da requisição deverão ser enviadas pelo **Body** da requisição e seguirá o formato.JSON, no seguinte formato .JSON:

```json
{
    "Name": "Alguem",
    "Sexo": "Feminino",
    "birthday": "2000-01-18",
    "Idade": 25,
    "CityId": "fde4eba9-3319-4147-8af8-4b866ef55e89"
}
```

CityId deverá ser um ID de alguma cidade EXISTENTE no banco de dados. Para conseguir um ID vide os [Listar cidade](#Listar-todas-as-cidades).

Sucess: HTTP status code 201 e o ID do cliente cadastrado no banco.

## Listar todos os clientes

Para listar todas as clientes existente no banco utilize uma requisição do tipo <b><span style = "color:green">GET</span></b> na rota **/clients**:

`http://localhost:5237/clients`

Para otimização da pesquisa em um banco com muitos registros, a rota possui dois argumentos opcionais do tipo **query params**.

* **page**: Tipo Int. Indica o número da página que você deseja exibir e dependerá da quantidade de registro existente na tabela. Por padrão o valor será de 1, indicando a primeira página;
* **rows**: Tipo Int. Indica a quantidades de registro que deseja por página, Padrão: 10. Max: 10

Se escolher passar todos os parâmetros o endereço será:

`http://localhost:5237/clients?page=1&rows=10`

O retorno será um HTTP status code 200 e um array de objetos:

## Pesquisar cliente pelo ID

Requisição do tipo <b><span style = "color:green">**GET**</span></b>, na rota **/clients/{id:guid}**, que recebe um argumento, via **ROUTE** do tipo **GUID** que deverá ser um ID de algum cliente existente no banco. 

Endereço de exemplo:

`http://localhost:5237/clients/88b4ab8d-fbbf-419b-99b9-5267e0517cb7o`

Se a requisição retornar um sucesso, retornará um HTTP status code 200 e um array de objetos:

```json
[
    {
        "id": "88b4ab8d-fbbf-419b-99b9-5267e0517cb7",
        "name": "Tereza",
        "sexo": "Feminino",
        "birthday": "1988-03-10T00:00:00",
        "idade": 35,
        "city": "Natal",
        "state": "Rio Grande do Norte"
    }
]
```

Se o parâmetro não for devidamente utilizado, ou seja, o parâmetro existe, mas nenhum valor foi informado, será retornado um HTTP status code 200 e um array de objetos contendo os registros de Clientes.

Se nenhum cliente for encontrado será retornado um HTTP status code 404.

## Pesquisar cliente pelo Nome

Requisição do tipo <b><span style = "color:green">**GET**</span></b>, na rota **/clients-by-name**, que **recebe um argumento do tipo string chamado name** que indica o nome do cliente que você deseja consultar. Endereço de exemplo

`http://localhost:5237/cities-by-name?name=NomeDaCidade`

Se a requisição retornar um sucesso será retornado um HTTP status code 200 e um array de objetos.

```json
[
    {
        "id": "52b9b60a-ab80-4821-95e6-d4db583ec937",
        "name": "São Paulo",
        "state": "São Paulo"
    }
]
```

Se nenhum cliente for encontrado será retornado um HTTP status code 404.

## Atualizar Cliente

Para atualizar um cliente, enviar uma requisição do tipo <b><span style = "color:BLUE">**PUT**</span></b> no endereço **/clients/{id:guid}**.

A rota recebe um argumento obrigatório via **ROUTE**, que deve ser um GUID correspondente ao ID de algum registro de cliente no banco de dados. Para conseguir um ID vide os [Listar clientes](#Listar-todos-os-clientes).

Ex.:

`http://localhost:5237/clients/ed8a8783-2165-4680-ad96-5cf66fdb59c6`

O payload deverá ser enviado em pelo body da requisição utilizando o formato.JSON.

```json
{
    "name": "Rhi 2",
    "sexo": "Feminino",
    "birthday": "2001-11-12",
    "idade": 36,
    "CityId": "fde4eba9-3319-4147-8af8-4b866ef55e89"
}
```

Sucesso: HTTP status code 200.

## Deletar Cliente

Para deletar um cliente, enviar uma requisição do tipo <b><span style = "color:pink">**PUT**</span></b> no endereço **/clients/{id:guid}**.

A rota recebe um argumento obrigatório via **ROUTE**, que deve ser um  GUID correspondente ao ID de algum clientedo banco de dados. Para conseguir um ID vide os [Listar clientes](#Listar-todos-os-clientes)

Ex.:

`http://localhost:5237/clients/2381aaaa-ee45-433a-b0b9-a0c2b1a819f1`

Sucess: HTTP status code 200.

Se nenhum ID for informado, será retornado um HTTP status code 405,

Se o ID informado não existir no banco será retornado um HTTP status code 404.

## Error

* Error to convert data:

  ```json
  {
      "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
      "title": "Error to convert data",
      "status": 500
  }
  ```

  Indica que algum dado indicado no momento da realização da requisição não está respeitando os seus tipo pré-definido. Confira os dados de sua requisição.

  * Database out: Erro indica que o banco de dados não está conectado.

  * An error occured: Analise o log no terminal.

    