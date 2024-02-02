using P2Poker.Bean;
using P2Poker.Enums;

namespace P2Poker.Entitys;

public class RoomController : BaseController
{
    public RoomController()
    {
        requestCode = RequestCode.Room;
    }

    public string CreateRoom(string data, Client client, Server server)
    {
        //server.CreateRoom(client);
        //return ((int)ReturnCode.Success).ToString()+","+((int)RoleType.Blue).ToString();
        return "";
    }

    public string ListRoom(string data, Client client, Server server)
    {
        /*
        StringBuilder sb = new StringBuilder();
        foreach(Room room in server.GetRoomList())
        {
            if (room.isWaitingJoin())
            {
                sb.Append(room.GetHouseOwnerData() + "|");
            }
        }
        if (sb.Length == 0)
        {
            sb.Append("0");
        }
        else
        {
            sb.Remove(sb.Length - 1, 1);
        }
        return sb.ToString();
        */
        return "";
    }

    public string JoinRoom(string data, Client client, Server server)
    {
        /*
        int id = int.Parse(data);
        Room room = server.GetRoomById(id);
        if (room == null)
        {
            return ((int)ReturnCode.NotFound).ToString();
        }
        else if (room.isWaitingJoin() == false)
        {
            return ((int)ReturnCode.Fail).ToString();
        }
        else
        {
            room.AddClient(client);
            string roomData = room.GetRoomData();
            room.BroadCastMessage(client, ActionCode.UpdateRoom, roomData);
            return ((int)ReturnCode.Success).ToString()+","+((int)RoleType.Red).ToString()+"-"+roomData;
        }
        */
        return "";
    }
}