using System.Diagnostics.CodeAnalysis;
using coe.dnd.api.Extensions;

namespace coe.dnd.api;

[ExcludeFromCodeCoverage] 
public static class EnvironmentVariables
{
    private static string DbConnectionStringKey => "DbConnectionString";

    public static string DbConnectionString => DbConnectionStringKey.GetValue("Server=localhost,5432;Database=coe-dnd-organiser;User Id=user;Password=password;");
}