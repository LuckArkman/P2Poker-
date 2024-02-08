using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using P2Poker.Enums;
using P2Poker.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
    [Obsolete("Obsolete")]
    public byte[] ConverteObjectEmByte(object? obj)
    {
        byte[] serializedBytes;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, obj);
            serializedBytes = memoryStream.ToArray();
            return serializedBytes;
        }

        return serializedBytes;
    }

    [Obsolete("Obsolete")]
    public T? ConverteByteEmObject<T>(byte[] bytes)
    {
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T obj = (T)binaryFormatter.Deserialize(memoryStream);
            return obj;
        }
    }

    T? DeserializeFromString<T>(string s)
    =>JsonConvert.DeserializeObject<T>(s);
}