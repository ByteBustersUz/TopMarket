using Data.IRepositories;
using Data.Repositories;
using Service.Interfaces;
using Service.Mappers;
using Service.Services;

namespace TopMarket.Extensions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
