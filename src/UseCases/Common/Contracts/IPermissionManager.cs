namespace UseCases.Common.Contracts;

public interface IPermissionManager
{
    Task<bool> HasPermission(string user, string relation, string obj);
    Task<bool> PutPermission(params (string user, string relation, string obj)[] permissions);
    Task<bool> RevokePermission(params (string user, string relation, string obj)[] permissions);
    Task<IEnumerable<string>> QueryAllObjectsInTypeDefinition(string user, string relation, string context);
}