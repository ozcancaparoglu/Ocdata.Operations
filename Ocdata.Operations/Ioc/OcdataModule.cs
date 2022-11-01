using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ocdata.Operations.Behaviours;
using Ocdata.Operations.Entities;
using Ocdata.Operations.Helpers.RestHelper;
using Ocdata.Operations.Repositories;
using Ocdata.Operations.Repositories.Contracts;
using System.Reflection;

namespace Ocdata.Operations.Ioc
{
    public static class OcdataModule
    {
        public static IServiceCollection OcdataServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
            services.AddScoped<IRestClientHelper, RestClientHelper>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMongoConfig, MongoConfig>();
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


            return services;
        }
        
    }
}
