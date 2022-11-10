# Ocdata.Operations

> Project For Common Data Operations (.NET 6.0)

> Nuget Package : Install-Package Ocdata.Operations -Version 1.1.7

> Example usage project : https://github.com/ozcancaparoglu/Marketplace.git

## Usage

- In Program.cs add

`builder.Services.OcdataServices();`

- Register your context with DbContext in your modules or in Program.cs

`services.AddScoped<DbContext, YourContext>(); `

- For Redis usage you should use RedisConfigurationOptions

`services.Configure<RedisConfigurationOptions>(configuration.GetSection("RedisDatabase"));`

 appsettings.json configuration: `"RedisDatabase": {"Host": "localhost","Port": "6379","Admin": "allowAdmin=true"},`
 
 - UnitOfWork `IUnitOfWork`
 - GenericRepository `await _unitOfWork.Repository<Category>().Find(x => x.Name.ToUpperInvariant() == dto.Name.ToUpperInvariant() 
 && x.DisplayName.ToUpperInvariant() == dto.DisplayName.ToUpperInvariant());`
 - CacheManager `ICacheService`
 `if (!_cacheService.TryGetValue(CacheConstants.CategoryCacheKey, out _allCategories))
            {
                AllCategories = (List<Category>)await _unitOfWork.Repository<Category>().GetAll();
                _cacheService.Add(CacheConstants.CategoryCacheKey, AllCategories, CacheConstants.CategoryCacheTime);
            }

            return AllCategories;`





