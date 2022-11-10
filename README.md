# Ocdata.Operations

> Project For Common Data Operations (.NET 6.0)

> Nuget Package : Install-Package Ocdata.Operations -Version 1.1.7

> Example usage project : https://github.com/ozcancaparoglu/Marketplace.git

## Usage

- In Program.cs add

`builder.Services.OcdataServices();`

- Register your context with DbContext in your modules or in Program.cs

`services.AddScoped<DbContext, YourContext>(); `

- UnitOfWork, GenericRepository, AutoMapper and CacheService

`       private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        private List<Category> _allCategories;
        public List<Category> AllCategories
        {
            get { return _allCategories; }
            set { _allCategories = value; }
        }

        public AsyncCategoryService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _allCategories = new List<Category>();
        }

        public async Task<IEnumerable<Category>> List()
        {
            if (!_cacheService.TryGetValue(CacheConstants.CategoryCacheKey, out _allCategories))
            {
                AllCategories = (List<Category>)await _unitOfWork.Repository<Category>().GetAll();
                _cacheService.Add(CacheConstants.CategoryCacheKey, AllCategories, CacheConstants.CategoryCacheTime);
            }

            return AllCategories;
        }

        public async Task<Category?> Save(CategoryDto dto)
        {
            var existing = await _unitOfWork.Repository<Category>().Find(x => x.Name.ToUpperInvariant() == dto.Name.ToUpperInvariant() 
            && x.DisplayName.ToUpperInvariant() == dto.DisplayName.ToUpperInvariant());

            if (existing != null)
                return null;

            var entity = _mapper.Map<Category>(dto);

            await _unitOfWork.Repository<Category>().Add(entity);

            _cacheService.Remove(CacheConstants.CategoryCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }

        public async Task<Category?> Update(CategoryDto dto)
        {
            var entity = await _unitOfWork.Repository<Category>().GetById(dto.Id);

            if (entity == null)
                return null;

            entity.SetCategory(dto.ParentId, dto.Name, dto.DisplayName, dto.Description);

            _unitOfWork.Repository<Category>().Update(entity);

            _cacheService.Remove(CacheConstants.CategoryCacheKey);

            await _unitOfWork.CommitAsync();

            return entity;
        }
  
`

- For Redis usage you should use RedisConfigurationOptions

`services.Configure<RedisConfigurationOptions>(configuration.GetSection("RedisDatabase"));`

 appsettings.json configuration: `"RedisDatabase": {"Host": "localhost","Port": "6379","Admin": "allowAdmin=true"},`
 





