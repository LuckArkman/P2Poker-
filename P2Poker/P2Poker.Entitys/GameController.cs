using Newtonsoft.Json;
using P2Poker.Bean;
using P2Poker.Enums;
using P2Poker.Interfaces;

namespace P2Poker.Entitys;

public class GameController : BaseController
{
    public Dictionary<Guid,IPlayer> clientsGO = new Dictionary<Guid,IPlayer>();

    public List<Card> _cards = new List<Card>();
    public GameController()
        => requestCode = RequestCode.Game;
    
    public void StartGame(List<IPlayer> clientsGo, IPlayer client, Room? room)
    {
        if (client.UserID == room!.client.UserID && clientsGO.Count >= 3) OnStartGame(clientsGO, room);
        clientsGO.Add(client.UserID, client);
        UserIsGo(client, room);
        room.client.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.StartGame, client.UserID.ToString())));
        if(clientsGO.Count >=3) room.client.SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.StartGame, client.UserID.ToString())));
    }

    private async void UserIsGo(IPlayer client, Room room)
    {
        var AllUser = room.clientList.FindAll(x => x.UserID != client.UserID);
        int i = 0;
        while (i < AllUser.Count)
        {
            await Task.Delay(50);
            AllUser[i].SendData(Message.PackData(new Msg(RequestCode.Game, ActionCode.StartGame, client.UserID.ToString())));
            i++;
        }
        await Task.CompletedTask;
    }

    private void OnStartGame(Dictionary<Guid, IPlayer> clientsGo, Room room)
    {
         string _textjson = "[{\"UUID\":\"252a10cb-54ab-4dc0-9475-b8ff8d964859\",\"value\":14},{\"UUID\":\"fc46af31-9910-4b1b-a004-8627946c0345\",\"value\":2},{\"UUID\":\"d56a79b9-513d-4619-9426-59ad74fd452e\",\"value\":3},{\"UUID\":\"298af580-9cd0-444b-a37f-c70bd0753e18\",\"value\":4},{\"UUID\":\"bc6196aa-be8d-4712-b6bd-c26c9bd5823f\",\"value\":5},{\"UUID\":\"0ebe4943-87b7-47b2-915b-850bcd28899d\",\"value\":6},{\"UUID\":\"8301ce73-dd95-45db-8cb1-826761ba63e1\",\"value\":7},{\"UUID\":\"7d2d2a73-741a-4316-b050-08117e205d45\",\"value\":8},{\"UUID\":\"1ff4edfa-8d29-49a3-abec-59d5ea1f2d10\",\"value\":9},{\"UUID\":\"e9ac31f4-ecf1-4466-a608-0a4e88312946\",\"value\":10},{\"UUID\":\"e67e5179-e7ef-4f33-83ce-2a512725a72f\",\"value\":11},{\"UUID\":\"e8d46789-0b99-45c8-9444-1e31f765ecf7\",\"value\":12},{\"UUID\":\"80e1fc14-917b-4ef4-a5de-1fb4284dfb62\",\"value\":13},{\"UUID\":\"1452a7e5-a331-48ff-9de0-d0d5542169d1\",\"value\":13},{\"UUID\":\"7bc491dc-71e8-4663-b136-f128117701fb\",\"value\":12},{\"UUID\":\"8bc2fd62-cb9b-4007-920c-bab8c5e53be9\",\"value\":11},{\"UUID\":\"30e5eaa2-8e2e-429a-af32-0a00c7325db4\",\"value\":10},{\"UUID\":\"2afb1ebc-8710-4895-a9a7-7e43d9b18ca5\",\"value\":9},{\"UUID\":\"2e01ca7c-a979-4584-8a52-ddc89fbb9c0d\",\"value\":8},{\"UUID\":\"b1042257-a162-4d21-85c7-1fdea2cff907\",\"value\":7},{\"UUID\":\"1e1d2aa8-a0f6-42a7-9deb-31169f058f03\",\"value\":6},{\"UUID\":\"2ab143f2-2b5d-4f7b-ae13-926a2de0a234\",\"value\":5},{\"UUID\":\"60a75658-f3f2-4ca3-aa65-1a2ab4c8ee9b\",\"value\":4},{\"UUID\":\"c805ad1c-1bc2-4967-b657-80d4656c112c\",\"value\":3},{\"UUID\":\"bd01be61-75c2-460d-b891-c5e8004731c0\",\"value\":2},{\"UUID\":\"e3f744b1-5b1d-4b33-a48e-b3a678957db3\",\"value\":14},{\"UUID\":\"2a490776-a053-498c-af09-38350b27a7df\",\"value\":14},{\"UUID\":\"3712379b-5aff-4af9-bae9-0642ff9b40cd\",\"value\":2},{\"UUID\":\"3febe13e-9e59-4992-b1f9-2d0a16f033a4\",\"value\":3},{\"UUID\":\"31b10641-9512-4685-b08e-33c524671722\",\"value\":4},{\"UUID\":\"bf25b117-9ce2-4fea-8f11-05b24366177c\",\"value\":5},{\"UUID\":\"b1849802-10ee-411e-b13a-5c88e29cfc9b\",\"value\":6},{\"UUID\":\"6297017f-24d7-47bc-a45f-a00664f8a7fc\",\"value\":7},{\"UUID\":\"a1d8092f-2010-4442-bfee-5c4cb2257f99\",\"value\":8},{\"UUID\":\"7ec8e306-96ae-4276-a4d5-a52ff2d0e052\",\"value\":9},{\"UUID\":\"fec24310-b936-400d-8cee-69deedf4e772\",\"value\":10},{\"UUID\":\"069c7395-3940-4f58-8065-a06b07938d29\",\"value\":11},{\"UUID\":\"9491609d-4b09-44b8-bbcf-9f77d0249055\",\"value\":12},{\"UUID\":\"53168253-3127-4faf-b429-83bb783284aa\",\"value\":13},{\"UUID\":\"8cb3b4ba-7b89-4073-9f94-41e943cb973c\",\"value\":13},{\"UUID\":\"4f6cb91b-5623-460c-b52a-c8b2eca985e4\",\"value\":12},{\"UUID\":\"6374fe63-4d6c-49fd-8d3c-4bf2fbce76fb\",\"value\":11},{\"UUID\":\"545921bb-d574-4c85-9f2c-f1cbb06517ce\",\"value\":10},{\"UUID\":\"ca106d5c-7d28-47e6-8062-3624b88a6247\",\"value\":9},{\"UUID\":\"4d144b54-0f82-41b9-8d30-07d3911571b5\",\"value\":8},{\"UUID\":\"6b0e3785-fa3d-4ea3-9081-1d0b2178244b\",\"value\":7},{\"UUID\":\"4980b599-d34b-4bd0-9bd2-a5991a9a4ad0\",\"value\":6},{\"UUID\":\"acdc6f52-4db2-4406-9b82-e12e1b5f8059\",\"value\":5},{\"UUID\":\"ec165e57-4025-485e-9682-57d7bc9c8e03\",\"value\":4},{\"UUID\":\"d2864139-8643-425a-8d5a-d1a026894582\",\"value\":3},{\"UUID\":\"fb083a86-dedf-4b63-b797-9aba2ee46852\",\"value\":2},{\"UUID\":\"695c7de1-fc33-4c5d-8071-460b45df8d4b\",\"value\":14}]";
    _cards = JsonConvert.DeserializeObject<List<Card>>(_textjson);
    if (room.cards.Count <= 0) room.cards = _cards;
    room.StartCards(clientsGo);
    }

    public void Action( IPlayer client,string data)
    {
    }
    public void Cobrir( IPlayer client,string data)
    {
    }
    public void Bet(IPlayer client,string data)
    {
    }

    public void Game(ActionCode actionCode, IPlayer client,  string data)
    {
        var room = client.roomController;
        if (actionCode == ActionCode.StartGame) StartGame(room.clientList, client, room);
        if (actionCode == ActionCode.Bet)Bet(client, data);
        if (actionCode == ActionCode.Cobrir)Cobrir(client, data);
        if (actionCode == ActionCode.Pass)Pass(client, data);
        if (actionCode == ActionCode.Check)Check(client, data);
    }

    private void Check(IPlayer client, string data)
    {
    }

    private void Pass(IPlayer client, string data)
    {
    }

    public async void SendCards()
    {
        List<IPlayer> players = new List<IPlayer>();
        foreach (var cl in clientsGO)
        {
            players.Add(cl.Value);
        }
        int i = 0;
        while (i < players.Count)
        {
            await Task.Delay(50);
            players[i].SendData(Message.PackData(new Msg(RequestCode.User, ActionCode.StartGame, JsonConvert.SerializeObject(players[i].handContext))));
            i++;
        }
    }
}