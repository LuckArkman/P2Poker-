using Microsoft.EntityFrameworkCore;
using P2Poker.Bean;
using P2Poker.Context;
using P2Poker.Entitys;
using P2Poker.Exceptions;
using P2Poker.Interfaces;

namespace P2Poker.Repository;

public class RoomRepository : IP2PokerRepository
{
    P2pokerDbContext _catalogDb;

    public RoomRepository(P2pokerDbContext dbContext)
        => _catalogDb = dbContext;

    public async Task Insert(Room room, CancellationToken cancellationToken)
    {
        await _catalogDb._rooms.AddRangeAsync(room);
        await _catalogDb.SaveChangesAsync(cancellationToken);
        await Task.CompletedTask;
    }

    public async Task InsertUser(Guid romId, Client client)
    {
        await Task.CompletedTask;
    }

    public async Task<Room?> Get(Guid Id, CancellationToken cancellationToken)
        =>await _catalogDb._rooms.FirstOrDefaultAsync(x => x.UUID == Id, cancellationToken)!;

    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        Room? room = await _catalogDb._rooms.FirstOrDefaultAsync(x => x.UUID == Id, cancellationToken);
        if (room is not null) _catalogDb._rooms.Remove(room);
        await Task.CompletedTask;
    }

    public async Task<Room?> Update(Room _room, CancellationToken cancellationToken)
    {
        Room? room = await _catalogDb._rooms.FirstOrDefaultAsync(x => x.UUID == _room.UUID, cancellationToken);
        NotFoundException.ThrowIfNull(room, $"Room '{room.UUID}' not found.");
        room = _room;
        return room;
    }
    public async Task<Room> Search(Guid input)
        => await Task.FromResult(new Room());

    public List<RoomManager> GetRooms()
    {
        List<RoomManager> r = new List<RoomManager>();
        foreach (var _room in _catalogDb._rooms)
        {
            r.Add(new RoomManager(_room.tableContext, _room.UUID, _room.PlayerButton, _room.turn));
        }
        return r;
    }
}