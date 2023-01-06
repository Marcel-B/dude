﻿using DevIt.Domain;
using DevIt.Pbi.Adapter;
using DevIt.Persistence;
using DevIt.Projekt.Adapter;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Application;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    return services
      .AddDomain()
      .AddProjektAdapter()
      .AddPbiAdapter()
      .AddPersistence();
  }
}