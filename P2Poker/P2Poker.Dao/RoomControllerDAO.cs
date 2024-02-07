using P2Poker.Bean;
using P2Poker.Entitys;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Strucs;
using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Dao;

    public class RoomControllerDAO : IBaseController, IRoomController
    {
        public GameController _gameController { get; set; }
        public Guid GetUUID() => UUID;
        public IStartTableContext tableContext { get; set; }
        public List<Guid> Tablecards = new List<Guid>();
        public Dictionary<string, Pot> _pot = new Dictionary<string, Pot>();
        public GameRoundType roundType;
        public Guid UUID { get; set; }
        public int PlayerButton { get; set; }
        public int turn { get; set; }
        public List<IPlayer> clientList = new List<IPlayer>();
        public Dictionary<Guid,IPlayer> clientsGO = new Dictionary<Guid,IPlayer>();
        public List<string> clearList = new List<string>();
        public List<string> cards = new List<string>();
        public Dictionary<Guid,ClientDAO> cardsList = new Dictionary<Guid,ClientDAO>();
        protected RequestCode requestCode = RequestCode.None;
        public IPlayer? client;
        
        public void StartCards()
        {
            
            SendCards();
        }

        public void Tablecard()
        {
            SendTableCards();
        }

        private async void SendTableCards(){}

        IEnumerator<Guid> GetCards(List<Guid> crd)
            =>  null;

        async void SendCards()
        {
        }
        public void NextTurn()
        =>throw new NotImplementedException();

        public void OnMessageFullRoom(string clientId)
        =>throw new NotImplementedException();

        public void OnPot(string client, string[] info)
        =>throw new NotImplementedException();

        public void OnStart()
        =>throw new NotImplementedException();

        public void SendRoomInfo(string clientId)
        =>throw new NotImplementedException();

        public void SendTurn(int playerTurn)
        =>throw new NotImplementedException();

        public void SetClient(string clientId)
        =>throw new NotImplementedException();

        public void SetStartGame(string clientId)
        =>throw new NotImplementedException();
        

        public void StartTimer(){}

        public void TakeBet(int damage, IPlayer client){}
        public bool IsPlayerWinner(IPlayer client)
            => false;

        public void BroadCastMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message)
            => clientList.ToList().ForEach(x=>
            {
                if (x.UserID != player.UserID) clientList.Find(u => u.UserID == x.UserID)!.SendData(Message.PackData(new Msg(requestCode, actionCode, message)));
            });
        public List<IPlayer> Clients()
            => clientList;

        public void SendMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message)
            => player.SendData(Message.PackData(new Msg(requestCode, actionCode, message)));
    }