﻿using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infra;

public static class DependencyInjectionHelper
{
	public static IServiceCollection AddDI(this IServiceCollection services, IConfiguration configuration)
	{
		AddRepositories(services);
		AddContext(services, configuration);

		return services;
	}

	private static void AddContext(IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<HackathonDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings:DefaultConnection")));
	}

	private static void AddRepositories(IServiceCollection services)
	{
		var types = Assembly.GetExecutingAssembly().GetTypes()
			.Where(x => x.GetInterfaces().Any(i => i.Name.EndsWith("Repository")));

		foreach (var type in types)
		{
			var interfaces = type.GetInterfaces();
			foreach (var inter in interfaces)
				services.AddScoped(inter, type);
		}
	}
}