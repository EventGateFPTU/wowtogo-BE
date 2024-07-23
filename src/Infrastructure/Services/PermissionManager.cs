using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Exceptions;
using OpenFga.Sdk.Model;
using UseCases.Common.Contracts;

namespace Infrastructure.Services;

public class PermissionManager(OpenFgaClient client) : IPermissionManager
{
    public async Task<bool> HasPermission(string user, string relation, string obj)
    {
        var body = new ClientCheckRequest {
            User = user,
            Relation = relation,
            Object = obj,
        };
        var res = await client.Check(body);

        return res.Allowed ?? false;
    }

    public async Task<bool> PutPermission(params (string user, string relation, string obj)[] permissions)
    {
        var body = new ClientWriteRequest
        {
            Writes = permissions.Select(x => new ClientTupleKey
            {
                User = x.user,
                Relation = x.relation,
                Object = x.obj,
            }).ToList(),
        };
        try
        {

            var res = await client.Write(body);
            return res.Writes.Count > 0;
        }
        catch (FgaApiValidationError ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> RevokePermission(params (string user, string relation, string obj)[] permissions)
    {
        var body = new ClientWriteRequest
        {
            Deletes = permissions.Select(x => new ClientTupleKeyWithoutCondition
            {
                User = x.user,
                Relation = x.relation,
                Object = x.obj,
            }).ToList(),
        };
        var res = await client.Write(body);

        return res.Deletes.Count > 0;
    }

    public async Task<IEnumerable<string>> QueryAllObjectsInTypeDefinition(string user, string relation, string context)
    {
        try
        {
            var body = new ClientListObjectsRequest
            {
                User = user,
                Relation = relation,
                Context = context
            };
            var res = await client.ListObjects(body);
            return res.Objects;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<IEnumerable<string>> ListUsersAsync(string type, string id, string relation)
    {
        var body = new ClientListUsersRequest
        {
            Object = new FgaObject
            {
                Type = type,
                Id = id,
            },
            Relation = relation,
            UserFilters = [new UserTypeFilter { Type = "user" }],
        };
        
        var response = await client.ListUsers(body);
        return response.Users.Where(x => x.Object is not null).Select(x => x.Object!.Id);
    }
}