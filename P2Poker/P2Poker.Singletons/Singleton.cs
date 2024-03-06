using Microsoft.EntityFrameworkCore;
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
            _DbContext = new P2pokerDbContext(
                new DbContextOptionsBuilder<P2pokerDbContext>()
                    .UseInMemoryDatabase($"integration-tests-db")
                    .Options
            );
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

    public async Task<Room?> RegisterRoom(Room output)
    {
        var db = _singleton().CreateDBContext();
        var repository = _singleton().CreateRoomRepository(db);
        await repository.Insert(output, CancellationToken.None);
        Room? room = await repository.Get(output.UUID, CancellationToken.None);
        if (room is not null)
        {
            return room;
        }
        else
        {
            return null;
        }
        
    }

    public async Task<Room> GetRoom(Guid id)
    {
        var db = _singleton().CreateDBContext();
        var repository = _singleton().CreateRoomRepository(db);
        Room? room = await repository.Get(id, CancellationToken.None);
        return room;
    }

    public List<RoomManager> GetAllRoom()
    {
        var db = _singleton().CreateDBContext();
        var repository = _singleton().CreateRoomRepository(db);
        var room = repository.GetRooms();
        return room;
    }
}