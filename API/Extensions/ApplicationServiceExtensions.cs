using Applications.Mapping;
using Applications.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices ( this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<FinancialContext> ( options =>
            options.UseNpgsql ( configuration.GetConnectionString ( "DefaultConnection" ) ) );

            services.AddAutoMapper(cfg => {}, typeof(CategoryMapping));
            services.AddScoped<ICategoryRepository, CategoryRepository> ( );
            services.AddScoped<ICategoryService, CategoryService> ( );
            services.AddScoped<IExpenseService, ExpenseService> ( );
            services.AddScoped<IExpenseRepository, ExpenseRepository> ( );
            services.AddScoped<IInstallmentExpenseService, InstallmentExpenseService> ( );
            services.AddScoped<IInstallmentExpenseRepository, InstallmentExpenseRepository> ( );
            services.AddScoped<IRecurringExpenseService, RecurringExpenseService> ( );
            services.AddScoped<IRecurringExpenseRepository, RecurringExpenseRepository> ( );

            return services;
        }
    }
}
