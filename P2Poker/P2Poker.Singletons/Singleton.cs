using P2Poker.Bean;
using P2Poker.Context;
using P2Poker.Repository;

namespace P2Poker.Singletons;

public class Singleton
{
    public P2pokerDbContext _DbContext { get; set; }
    public RoomRepository _RoomRepository { get; set; }

    public Singleton(){}

    public static Singleton instace { get; set; }

    public static Singleton _singleton()
    {
        if (instace is null) return instace = new();
        return instace;
    }

    public P2pokerDbContext CreateDBContext()
    {
        if (_DbContext is null)
        {
            _DbContext = new();
            return _DbContext;
        }
        return _DbContext;
    }

    public RoomRepository CreateRoomRepository(P2pokerDbContext _context)
    {
        if (_RoomRepository is null)
        {
            _RoomRepository = new RoomRepository(_context);
            return _RoomRepository;
        }

        return _RoomRepository;
    }

    public Room? RegisterRoom(Room output)
    {
        var db = _singleton().CreateDBContext();
        var repository = _singleton().CreateRoomRepository(db);
        repository.Insert(output);
        Room room = repository.Get(output.UUID);
        return room;
    }

    public Room? GetRoom(Guid id)
    => _singleton().CreateRoomRepository(_singleton().CreateDBContext()).Get(id);

    public List<Room> GetAllRoom()
    {
        var db = _singleton().CreateDBContext();
        var repository = _singleton().CreateRoomRepository(db);
        var room = repository.GetRooms();
        return room;
    }
}