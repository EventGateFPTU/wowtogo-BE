using System.Text.Json;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Model;

namespace Infrastructure.Extensions;

public static class OpenFgaExtension
{
    public static OpenFgaClient ConfigureAuthModel(this OpenFgaClient client)
    {
        if (client.AuthorizationModelId is not null && client.StoreId is not null)
            return client;
        var store = client.CreateStore(new ClientCreateStoreRequest() { Name = "FGA WowToGo Store" }).Result.Id;
        try
        {
            var modelJson = File.ReadAllText(Directory.GetCurrentDirectory() + "/authModel.json");
            Console.WriteLine(modelJson);
            var body = JsonSerializer.Deserialize<ClientWriteAuthorizationModelRequest>(modelJson);
            if (body is null)
                return client;
            var authorizationModel = client.WriteAuthorizationModel(body, new ClientWriteOptions
            {
                StoreId = store
            }).Result.AuthorizationModelId;

            client.StoreId = store;
            client.AuthorizationModelId = authorizationModel;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return client;
        }
        return client;
    }
}