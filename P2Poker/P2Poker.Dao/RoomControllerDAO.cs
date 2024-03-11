using System.Diagnostics;
using System.Reflection.Metadata;
using P2Poker.Bean;
using P2Poker.Entitys;
using P2Poker.Enums;
using P2Poker.Interfaces;
using P2Poker.Strucs;
using P2PokerAPI.P2Poker.Core;

namespace P2Poker.Dao
{
    public class RoomControllerDAO : IBaseController, IRoomController
    {
        public int _PlayerTurno = 1; 
        public GameController _gameController { get; set; }
        public Guid GetUUID() => UUID;
        public IStartTableContext tableContext { get; set; }
        public List<Guid> Tablecards = new List<Guid>();
        public Dictionary<string, Pot> _pot = new Dictionary<string, Pot>();
        public GameRoundType roundType;
        public Guid UUID { get; set; }
        public int PlayerButton { get; set; }
        public int turn { get; set; }
        public List<IPlayer> clientList = new();
        public Dictionary<Guid, IPlayer> clientsGO = new Dictionary<Guid, IPlayer>();
        public List<string> clearList = new List<string>();
        public List<Card> cards = new List<Card>();
        public Dictionary<Guid, ClientDAO> cardsList = new Dictionary<Guid, ClientDAO>();
        protected RequestCode requestCode = RequestCode.None;
        public IPlayer? client;

        public void StartCards(Dictionary<Guid, IPlayer> _clientsGo)
        {
            clientsGO = _clientsGo;
            ShortCards(clientsGO);
            SendCards();
        }

        void ShortCards(Dictionary<Guid, IPlayer> clientsGo)
        {
            foreach (var c in clientsGo)
            {
                if (c.Value.handContext is null) OnStartHand(c.Value);
            }

            _gameController.SendCards(cards);
        }

        private void OnStartHand(IPlayer player)
        {
            Card _card1 = GetCard();
            Card _card2 = GetCard();
            while (_card1.UUID == _card2.UUID)
            {
                _card1 = GetCard();
                _card2 = GetCard();
            }

            player.handContext = new StartHandContext(_card1.UUID, _card2.UUID, player.UserID, player.handNumber,
                player.PlayerCoins);
            lock (cards)
            {
                cards.Remove(_card1);
            }

            lock (cards)
            {
                cards.Remove(_card2);
            }
        }

        private Card GetCard()
        {
            Random rnd = new Random();
            int num = rnd.Next(cards.Count);
            return cards[num];
        }

        public void Tablecard()
        {
            SendTableCards();
        }

        private async void SendTableCards()
        {
        }

        IEnumerator<Guid> GetCards(List<Guid> crd)
            => null;

        async void SendCards()
        {
        }

        public void NextTurn()
            => throw new NotImplementedException();

        public void OnMessageFullRoom(string clientId)
            => throw new NotImplementedException();

        public void OnPot(string client, string[] info)
            => throw new NotImplementedException();

        public void OnStart()
            => throw new NotImplementedException();

        public void SendRoomInfo(string clientId)
            => throw new NotImplementedException();

        public void SendTurn(int playerTurn)
            => throw new NotImplementedException();

        public void SetClient(string clientId)
            => throw new NotImplementedException();

        public void SetStartGame(string clientId)
            => throw new NotImplementedException();


        public void StartTimer()
        {
        }

        public void TakeBet(int damage, IPlayer client)
        {
        }

        public bool IsPlayerWinner(IPlayer client)
            => false;

        public void BroadCastMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message)
        {
            var xl = clientList.ToList().FindAll(x => x.UserID != player.UserID);
            xl.ForEach(x => { x.SendData(Message.PackData(new Msg(requestCode, actionCode, message))); });

        }

        public void SendMessage(IPlayer player, RequestCode requestCode, ActionCode actionCode, string message)
            => player.SendData(Message.PackData(new Msg(requestCode, actionCode, message)));
    }

    public class UserClients
    {
        public UserClients(){}
        public Guid _guid{ get; set; }
        public IPlayer _player{ get; set; }

        public UserClients(Guid guid, IPlayer player)
        {
            _guid = guid;
            _player = player;
        }
    }
}
