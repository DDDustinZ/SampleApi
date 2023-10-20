using System;
using CompanyName.SampleApi.Application.Interfaces.HttpClients;

namespace CompanyName.SampleApi.Infrastructure.HttpClients;

public class UserHttpClient : IUserHttpClient
{
    public User GetUser(Guid requestUserId)
    {
        throw new NotImplementedException();
    }
}

