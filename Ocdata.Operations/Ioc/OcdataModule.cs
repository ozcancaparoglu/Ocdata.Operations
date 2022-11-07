using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ocdata.Operations.Behaviours;
using Ocdata.Operations.Cache;
using Ocdata.Operations.Cache.Redis;
using Ocdata.Operations.Entities;
using Ocdata.Operations.Helpers.RestHelper;
using Ocdata.Operations.Middlewares;
using Ocdata.Operations.Persistence;
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddScoped<IRestClientHelper, RestClientHelper>();
            services.AddSingleton<RedisServer>();
            services.AddScoped<ICacheService, RedisCacheService>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IMongoConfig, MongoConfig>();
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


            return services;
        }
        
    }
}
