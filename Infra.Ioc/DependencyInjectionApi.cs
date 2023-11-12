using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infra.Data.Repositories;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Services;
using Application.Mappings;
using Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Account;
using MediatR;

namespace Infra.Ioc
{
    public static class DependencyInjectionApi
    {
        public static IServiceCollection AddInfrastructureApi(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
                ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAlunoService, AlunoService>();

            //services.AddScoped<IAuthenticate, AuthenticateService>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(myhandlers);

            return services;
        }
    }
}