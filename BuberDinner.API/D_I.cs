using BuberDinner.API.Common.Mapping;
using BuberDinner.API.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.API;
public static class D_I
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
        services.AddControllers();
        services.AddMappings();
        return services;
    }
}
