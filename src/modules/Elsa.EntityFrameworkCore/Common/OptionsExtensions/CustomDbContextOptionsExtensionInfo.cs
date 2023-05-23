using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Elsa.EntityFrameworkCore.Common;

public class CustomDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
{
    public CustomDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension)
        : base(extension)
    {
    }

    public override bool IsDatabaseProvider => false;

    public override string LogFragment => "";

    public override int GetServiceProviderHashCode()
    {
        // Return a unique hash code for your custom extension
        return 0; // Replace with your own implementation
    }

    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
        // Add any debug information about your custom extension to the dictionary
        //debugInfo["CustomExtension:CustomOption:SchemaName"] = ((ElsaDbContextOptionsExtension)Extension).Options.SchemaName;
    }

    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
    {
        return true;
    }
}