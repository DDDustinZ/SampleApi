using Lamar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.SampleApi.Api.Controllers;

[ApiController]
[Route("/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    [SetterProperty] 
    protected IMediator Mediator { get; set; } = null!;
}