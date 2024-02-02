using System.Text;
using Newtonsoft.Json;
using P2Poker.Enums;
using P2Poker.Exceptions;

namespace P2Poker.Bean;

public class Message
{
    public Message(){}
    public static byte[] PackData(object? obj)
    {
        NotFoundException.ThrowIfNull(obj, $"{obj} Is Null");
        string msg = JsonConvert.SerializeObject(obj);
        return Encoding.UTF8.GetBytes(msg);
    }
    
    public void ReadMessage(string _string, Action<RequestCode, ActionCode, string> processDataCallback)
    {
        var obj = DeserializeFromString<Msg>(_string);
        NotFoundException.ThrowIfNull(obj, $"{obj} Is not Deserialize From String");
        processDataCallback(obj.requestCode, obj.actionCode, obj._string);
    }

    private T? DeserializeFromString<T>(string s)
        => JsonConvert.DeserializeObject<T>(s);
}