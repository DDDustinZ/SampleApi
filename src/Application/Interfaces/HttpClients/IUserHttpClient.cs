using System;

namespace CompanyName.SampleApi.Application.Interfaces.HttpClients;

public interface IUserHttpClient
{
    User GetUser(Guid requestUserId);
}

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}