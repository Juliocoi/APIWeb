﻿namespace APIWeb.EndPoints.Clients;

public record ClientResponse(Guid Id, string Name, string Sexo, DateTime Birthday, int Idade, string City, string State);
            