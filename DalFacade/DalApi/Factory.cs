namespace DalApi;

using System.Diagnostics;
using System.Reflection;
using static DalApi.DalConfig;
public static class Factory
{
    public static IDal? Get()
    {
        string dalType = s_dalName
            ?? throw new DO.DalConfigException($"DAL name is not extracted from the configuration");
        string dal = s_dalPackages[dalType]
           ?? throw new DO.DalConfigException($"Package for {dalType} is not found in packages list");
        try
        {
            Assembly.Load(dal ?? throw new DO.DalConfigException($"Package {dal} is null"));
        }
        catch (Exception)
        {
            throw new DO.DalConfigException("Failed to load {dal}.dll package");
        }
        Type? type = Type.GetType($"Dal.{dal}, {dal}")
            ?? throw new DO.DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");
        return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                   .GetValue(null) as IDal
            ?? throw new DO.DalConfigException($"Class {dal} is not singleton or Instance property not found");
    }
}

