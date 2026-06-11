using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VectorRefineLab.Console.Infrastructure.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<VectorRefineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

using var host = builder.Build();

System.Console.WriteLine("VectorRefineLab ready.");
