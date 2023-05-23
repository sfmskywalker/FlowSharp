﻿using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Common;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Elsa.EntityFrameworkCore.Extensions;

/// <summary>
/// Extends EF Core features.
/// </summary>
[PublicAPI]
public static partial class Extensions
{
    public static EFCoreWorkflowDefinitionPersistenceFeature UsePostgreSql(this EFCoreWorkflowDefinitionPersistenceFeature feature, string connectionString)
    {
        feature.DbContextOptionsBuilder = (_, db) => db.UseElsaPostgreSql(connectionString);
        return feature;
    }
    
    public static EFCoreWorkflowInstancePersistenceFeature UsePostgreSql(this EFCoreWorkflowInstancePersistenceFeature feature, string connectionString)
    {
        feature.DbContextOptionsBuilder = (_, db) => db.UseElsaPostgreSql(connectionString);
        return feature;
    }
    
    public static EFCoreWorkflowManagementPersistenceFeature UsePostgreSql(this EFCoreWorkflowManagementPersistenceFeature feature, string connectionString, ElsaDbContextOptions options = default)
    {
        feature.DbContextOptionsBuilder = (_, db) => db.UseElsaDbContextOptions(options).UseElsaPostgreSql(connectionString);
        return feature;
    }
}